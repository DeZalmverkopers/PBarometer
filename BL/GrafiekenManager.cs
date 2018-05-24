using DAL;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.Gemonitordeitems;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;


namespace BL
{
  public class GrafiekenManager
  {
    private GrafiekenRepository repository;
    private UnitOfWorkManager uowManager;

    //public Deelplatform HuidigDeelplatform
    //{
    //  get
    //  {
    //    return new DeelplatformenManager().GetDeelplatformByURL(RouteData.Values["deelplatform"].ToString());
    //  }
    //}

    public GrafiekenManager()
    {
    }

    public void AddGrafiek(Grafiek grafiek)
    {
      InitNonExistingRepo();
      repository.CreateGrafiek(grafiek);
    }

    //public IEnumerable<Grafiek> GetGrafieken(bool dashboard = false, bool items = false)
    //{
    //  InitNonExistingRepo();
    //  return repository.ReadGrafieken(dashboard, items);
    //}

    //public IEnumerable<Grafiek> GetGrafieken(int dashboardId, int deelplatformId)
    //{
    //  InitNonExistingRepo();
    //  return repository.ReadGrafieken(dashboardId, deelplatformId);
    //}

    public IEnumerable<Grafiek> GetGrafieken(int dashboardId, int deelplatformId, bool legeLegende)
    {
      InitNonExistingRepo();
      GemonitordeItemsManager itemsManager = new GemonitordeItemsManager();
      List<GemonitordItem> alleItems = itemsManager.GetGemonitordeItems(deelplatformId).ToList();
      List<Grafiek> grafieken = repository.ReadGrafieken(dashboardId, false).ToList();

      foreach (Grafiek grafiek in grafieken)
      {
        List<GemonitordItem> grafiekItems = new List<GemonitordItem>();

        #region Kijk na welke items tot de grafiek behoren, vul de grafiekItems met items en hun historieken
        foreach (GrafiekItem item in grafiek.GrafiekItems)
        {
          foreach (GemonitordItem gevuldItem in alleItems)
          {
            if (gevuldItem.GemonitordItemId == item.ItemId)
            {
              grafiekItems.Add(gevuldItem);
            }
          }
        }
        #endregion

        #region Voeg de waarden van de historieken en plaats ze in de List
        if (grafiek.Type.Equals("line"))
        {

          foreach (GemonitordItem item in grafiekItems)
          {
            List<ItemHistoriek> historieken = item.ItemHistorieken;
            List<double> waarden = new List<double>();
            List<double> waarden2 = new List<double>();

   

            for (int i = historieken.Count - grafiek.Periode; i < historieken.Count; i++)
            {

             

              if (grafiek.GrafiekWaarde == GrafiekWaarde.Vermeldingen)
              {
                waarden.Add(historieken[i].AantalVermeldingen);

              }
              if (grafiek.GrafiekWaarde == GrafiekWaarde.Polariteit)
              {
                waarden.Add(historieken[i].GemPolariteit);

              }
              if (grafiek.GrafiekWaarde == GrafiekWaarde.Objectiviteit)
              {
                waarden.Add(historieken[i].GemObjectiviteit);

              }

              if (grafiek.GrafiekWaarde == GrafiekWaarde.VermeldingenMannen)
              {
                waarden.Add(historieken[i].AantalBerichtenVanMannen);

              }

              if (grafiek.GrafiekWaarde == GrafiekWaarde.VermeldingenVrouwen)
              {
                waarden.Add(historieken[i].AantalBerichtenVanVrouwen);

              }

              if (grafiek.GrafiekWaarde == GrafiekWaarde.VermeldingenMannenVrouwen)
              {

                waarden.Add(historieken[i].AantalBerichtenVanMannen);
                waarden2.Add(historieken[i].AantalBerichtenVanVrouwen);

              }

            }


            if (grafiek.GrafiekWaarde == GrafiekWaarde.VermeldingenMannenVrouwen)
            {
              grafiek.LegendeLijst.Add("mannen");
              grafiek.LegendeLijst.Add("vrouwen");
              grafiek.Datawaarden.Add(waarden);
              grafiek.Datawaarden.Add(waarden2);
            }
            else
            {
              grafiek.LegendeLijst.Add(item.Naam);
              grafiek.Datawaarden.Add(waarden);
            }


          }


          for (int i = grafiekItems[0].ItemHistorieken.Count - grafiek.Periode; i < grafiekItems[0].ItemHistorieken.Count; i++)
          {
            grafiek.XLabels.Add(grafiekItems[0].ItemHistorieken[i].HistoriekDatum.ToShortDateString());

          }

        }

        if (grafiek.Type.Equals("bar"))
        {
          List<double> waarden = new List<double>();
          foreach (GemonitordItem item in grafiekItems)
          {

            grafiek.XLabels.Add(item.Naam);

            if (legeLegende)
            {
              grafiek.LegendeLijst.Add(null);
            }
            else
            {
              grafiek.LegendeLijst.Add(item.Naam);
            }

            if (grafiek.GrafiekWaarde == GrafiekWaarde.Vermeldingen) waarden.Add(item.TotaalAantalVermeldingen);
            if (grafiek.GrafiekWaarde == GrafiekWaarde.Polariteit) waarden.Add(item.GemPolariteit);
            if (grafiek.GrafiekWaarde == GrafiekWaarde.Objectiviteit) waarden.Add(item.GemObjectiviteit);
          }
          grafiek.Datawaarden.Add(waarden);

        }

        if (grafiek.Type.Equals("pie"))
        {
          List<double> waarden = new List<double>();
          foreach (GemonitordItem item in grafiekItems)
          {

            grafiek.XLabels.Add(item.Naam);
            grafiek.LegendeLijst.Add(item.Naam);

            if (grafiek.GrafiekWaarde == GrafiekWaarde.Vermeldingen) waarden.Add(item.TotaalAantalVermeldingen);
            if (grafiek.GrafiekWaarde == GrafiekWaarde.Polariteit) waarden.Add(item.GemPolariteit);
            if (grafiek.GrafiekWaarde == GrafiekWaarde.Objectiviteit) waarden.Add(item.GemObjectiviteit);
          }
          grafiek.Datawaarden.Add(waarden);

        }
        #endregion
      }

      return grafieken;
    }

    //public IEnumerable<Grafiek> GetGrafieken(int dashboardId, int deelplatformId)
    //{
    //  InitNonExistingRepo();
    //  return repository.ReadGrafieken(dashboard, items).Where(a => a.Dashboard != null && a.Dashboard.DashboardId.Equals(dashboardId));
    //}


    //public List<Grafiek> GetGrafieken(int deelplatformId, int dashboardId, bool dashboard = false, bool items = false)
    //{
    //  InitNonExistingRepo();
    //  GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
    //  List<GemonitordItem> alleItems = itemManager.GetGemonitordeItems(deelplatformId).ToList();
    //  List<Grafiek> grafieken = repository.ReadGrafieken(dashboardId, dashboard, items).ToList();

    //  foreach (Grafiek grafiek in grafieken)
    //  {
    //    List<GemonitordItem> grafiekItems = new List<GemonitordItem>();

    //    #region Kijk na welke items tot de grafiek behoren, vul de grafiekItems met items en hun historieken
    //    foreach (GrafiekItem item in grafiek.GrafiekItems)
    //    {
    //      foreach (GemonitordItem gevuldItem in alleItems)
    //      {
    //        if (gevuldItem.GemonitordItemId == item.GrafiekItemId)
    //        {
    //          grafiekItems.Add(gevuldItem);
    //          grafiek.Items.Add(gevuldItem); //Mogelijk niet nodig
    //        }
    //      }
    //    }
    //    #endregion

    //    #region Zoek naar de benodigde historieken en bereken de waarden die in de Dictionary moeten komen
    //    int sleutel = 0;
    //    DateTime huidigeTijd = DateTime.Now;
    //    foreach (GemonitordItem item in grafiekItems)
    //    {
    //      List<ItemHistoriek> historieken = new List<ItemHistoriek>();
    //      List<dynamic> waarden = new List<dynamic>();

    //      #region Zoek naar de historieken
    //      foreach (ItemHistoriek historiek in item.ItemHistorieken)
    //      {
    //        if ((huidigeTijd - historiek.HistoriekDatum).Days <= grafiek.Periode)
    //        {
    //          historieken.Add(historiek);
    //        }
    //      }
    //      #endregion

    //      #region Voeg de waarden van de historieken en plaats ze in de Dictionary
    //      GrafiekWaarde huidigeWaarde = grafiek.Waarden.ElementAt(sleutel);

    //      if (grafiek.Keuze == GrafiekKeuze.EvolutieAantalVermeldingen1Item)
    //      {
    //        foreach (ItemHistoriek historiek in historieken)
    //        {
    //          waarden.Add(historiek.AantalVermeldingen);
    //          grafiek.XLabels.Add(historiek.HistoriekDatum);
    //        }
    //        //grafiek.Type = GrafiekType.line;

    //        grafiek.Type = "line";
    //        grafiek.LegendeLijst.Add(item.Naam);
    //      }

    //      if (grafiek.Keuze == GrafiekKeuze.VergelijkingItemsDoorheenDeTijd)
    //      {
    //        foreach (ItemHistoriek historiek in historieken)
    //        {
    //          if (huidigeWaarde == GrafiekWaarde.Vermeldingen) waarden.Add(historiek.AantalVermeldingen);
    //          if (huidigeWaarde == GrafiekWaarde.Polariteit) waarden.Add(historiek.GemPolariteit);
    //          if (huidigeWaarde == GrafiekWaarde.Objectiviteit) waarden.Add(historiek.GemObjectiviteit);
    //          grafiek.XLabels.Add(historiek.HistoriekDatum);
    //        }
    //        grafiek.Type = "line";
    //        grafiek.LegendeLijst.Add(item.Naam + " - " + huidigeWaarde);
    //      }

    //      if (grafiek.Keuze == GrafiekKeuze.VergelijkingItemsOp1Moment)
    //      {
    //        foreach (ItemHistoriek historiek in historieken)
    //        {
    //          if (huidigeWaarde == GrafiekWaarde.Vermeldingen) waarden.Add(historiek.AantalVermeldingen);
    //          if (huidigeWaarde == GrafiekWaarde.Polariteit) waarden.Add(historiek.GemPolariteit);
    //          if (huidigeWaarde == GrafiekWaarde.Objectiviteit) waarden.Add(historiek.GemObjectiviteit);
    //          grafiek.XLabels.Add(item.Naam);
    //        }
    //        grafiek.Type = "bar";
    //      }

    //      if (grafiek.Keuze == GrafiekKeuze.KruisingTaart)
    //      {
    //        grafiek.Type = "pie";
    //      }

    //      if (grafiek.Keuze == GrafiekKeuze.KruisingBar)
    //      {
    //        grafiek.Type = "bar";
    //      }

    //      //grafiek.Data.Add(sleutel, waarden);
    //      //Sleutel verhogen na het toevoegen om bugs te vermijden
    //      ++sleutel;
    //      #endregion
    //    }
    //    #endregion
    //  }

    //  return grafieken;
    //}

    //public IEnumerable<Grafiek> GetGrafieken(int dashboardId, bool dashboard = false, bool items = false)
    //{
    //  InitNonExistingRepo();
    //  return repository.ReadGrafieken(dashboard, items).Where(a => a.Dashboard != null && a.Dashboard.DashboardId.Equals(dashboardId));
    //}



    public Grafiek GetGrafiek(int id, bool dashboard = false, bool items = false)
    {
      InitNonExistingRepo();
      return repository.ReadGrafiek(id, dashboard, items);
    }

    public void ChangeGrafiek(Grafiek grafiek)
    {
      InitNonExistingRepo();
      repository.UpdateGrafiek(grafiek);
    }

    public void RemoveGrafiek(Grafiek grafiek)
    {
      InitNonExistingRepo();
      repository.DeleteGrafiek(grafiek);
    }
    public void InitNonExistingRepo(bool uow = false)
    {
      if (uow)
      {
        if (uowManager == null)
        {
          uowManager = new UnitOfWorkManager();
          repository = new GrafiekenRepository(uowManager.UnitOfWork);
        }
      }
      else
      {
        repository = repository ?? new GrafiekenRepository();
      }
    }

  }
}