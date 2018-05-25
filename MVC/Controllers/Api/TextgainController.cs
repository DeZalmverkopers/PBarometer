using BL;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MVC.Controllers.Api
{
    public class TextgainController : ApiController, IDataController
    {

        public void HaalBerichtenOp(Deelplatform deelplatform)
        {
            AlertManager alertManager = new AlertManager();
            GemonitordeItemsManager gemonitordeItemsManager = new GemonitordeItemsManager();
            DeelplatformenManager deelplatformenManager = new DeelplatformenManager();
            DateTime sinceDatum;
            DateTime nieuweSyncDatum = DateTime.Now;

            deelplatform.LaatsteSynchronisatie = nieuweSyncDatum;
            deelplatformenManager.ChangeDeelplatform(deelplatform);

            //Een dictionary bestaande uit de naam van het thema en de kernwoorden van het thema. Deze wordt meegegeven met de request naar textgain.
            var themaDict = new Dictionary<string, List<string>>();
            foreach (var item in gemonitordeItemsManager.GetThemas(deelplatform.DeelplatformId).ToList())
            {
                var thema = item as Thema;
                themaDict.Add(thema.Naam, thema.KernWoorden);
            }
            string themaDictJson = JsonConvert.SerializeObject(themaDict);
            string uri = "https://kdg.textgain.com/query";
            List<GemonitordItem> personen = gemonitordeItemsManager.GetPersonen(deelplatform.DeelplatformId).ToList();
            List<DetailItem> detailItems = new List<DetailItem>();

            using (HttpClient http = new HttpClient())
            {
                //Berichten worden per persoon opgehaald
                foreach (var persoon in personen)
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                    request.Headers.TryAddWithoutValidation("X-API-Key", "aEN3K6VJPEoh3sMp9ZVA73kkr");
                    //sinceDatum is de datum vanaf wanneer er berichten mogen opgehaald worden.
                    if (persoon.DetailItems.Count > 0 && persoon.DetailItems.OrderByDescending(a => a.BerichtDatum).FirstOrDefault().BerichtDatum > nieuweSyncDatum.AddDays(-deelplatform.AantalDagenHistoriek).Date)
                    {
                        sinceDatum = persoon.DetailItems.OrderByDescending(a => a.BerichtDatum).FirstOrDefault().BerichtDatum;
                    }
                    else
                    {
                        sinceDatum = DateTime.Now.AddDays(-deelplatform.AantalDagenHistoriek).Date;
                    }
                    Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>
                    {
                        { "name", persoon.Naam },
                        { "since",  sinceDatum},
                        { "themes", themaDict}
                    };
                    string contentJson = JsonConvert.SerializeObject(parameters);
                    request.Content = new StringContent(contentJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = http.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // De berichten van textgain worden omgezet naar DetailItems
                        var contentAsString = response.Content.ReadAsStringAsync().Result;

                        var berichten = JArray.Parse(contentAsString);

                        foreach (var bericht in berichten.Children())
                        {
                            var dict = bericht.ToObject<Dictionary<string, dynamic>>();
                            var sentimentPolObj = new Dictionary<string, double>();
                            var andereEigenschappen = new Dictionary<string, List<string>>();
                            var profielEigenschappen = new Dictionary<string, string>();
                            var themas = new List<string>();
                            double objectiviteit = 0;
                            double polariteit = 0;

                            foreach (var item in dict)
                            {
                                if (item.Key.Equals("profile", StringComparison.OrdinalIgnoreCase))
                                {
                                    profielEigenschappen = item.Value.ToObject<Dictionary<string, string>>();
                                }
                                else if (item.Key.Equals("sentiment", StringComparison.OrdinalIgnoreCase))
                                {
                                    List<double> sentimentLijst = item.Value.ToObject<List<double>>();
                                    if (sentimentLijst.Count == 2)
                                    {
                                        polariteit = sentimentLijst[0];
                                        objectiviteit = sentimentLijst[1];
                                    }
                                    else
                                    {
                                        //Neutrale waardes geven wanneer sentiment niet beschikbaar is
                                        polariteit = 0;
                                        objectiviteit = 0.5;
                                    }
                                }
                                else if (item.Key.Equals("themes", StringComparison.OrdinalIgnoreCase))
                                {
                                    themas = item.Value.ToObject<List<string>>();
                                }
                                //Om het generiek te houden stoppen we extra eigenschappen in een Dictionary<string, List<string>>()
                                else if (!(item.Value is IList))
                                {
                                    andereEigenschappen.Add(item.Key, new List<string>() { item.Value.ToString() });
                                }
                                else if (item.Value is IList)
                                {
                                    andereEigenschappen.Add(item.Key, item.Value.ToObject<List<string>>());
                                }
                            }

                            DateTime.TryParse(andereEigenschappen["date"][0], out DateTime berichtDatum);
                            DetailItem detailItem = new DetailItem
                            {
                                BerichtDatum = berichtDatum,
                                Polariteit = polariteit,
                                Objectiviteit = objectiviteit,
                                AndereEigenschappen = andereEigenschappen,
                                ProfielEigenschappen = profielEigenschappen,
                                Themas = themas,
                                DeelplatformId = deelplatform.DeelplatformId
                            };
                            persoon.DetailItems.Add(detailItem);
                            gemonitordeItemsManager.ChangeGemonitordItem(persoon);
                        }
                    }
                }
            }
            gemonitordeItemsManager.RefreshItems(nieuweSyncDatum, deelplatform.AantalDagenHistoriek, deelplatform.DeelplatformId);
            AndroidController androidController = new AndroidController();
            androidController.StuurMobieleAlerts(deelplatform);
        }
    }
}