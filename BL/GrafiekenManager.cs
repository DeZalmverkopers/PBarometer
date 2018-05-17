using DAL;
using Domain.Dashboards;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public class GrafiekenManager
  {
    private GrafiekenRepository repository;
    private UnitOfWorkManager uowManager;


    public GrafiekenManager()
    {
    }

    public void AddGrafiek(Grafiek grafiek)
    {
      InitNonExistingRepo();
      repository.CreateGrafiek(grafiek);
    }

    public IEnumerable<Grafiek> GetGrafieken(bool dashboard = false, bool items = false)
    {
      InitNonExistingRepo();
      return repository.ReadGrafieken(dashboard, items);
    }

    public List<Grafiek> GetGrafieken(int deelplatformId, int dashboardId, bool dashboard = false, bool items = false)
    {
      InitNonExistingRepo();
      GemonitordeItemsManager itemManager = new GemonitordeItemsManager();
      List<GemonitordItem> alleItems = itemManager.GetGemonitordeItems(deelplatformId).ToList();
      List<Grafiek> grafieken = repository.ReadGrafieken(dashboardId, dashboard, items).ToList();

      foreach (Grafiek grafiek in grafieken)
      {
        List<GemonitordItem> grafiekItems = new List<GemonitordItem>();

        #region Kijk na welke items tot de grafiek behoren, vul de grafiekItems met items en hun historieken
        foreach (GrafiekItem item in grafiek.GrafiekItems)
        {
          foreach (GemonitordItem gevuldItem in alleItems)
          {
            if (gevuldItem.GemonitordItemId == item.GrafiekItemId)
            {
              grafiekItems.Add(gevuldItem);
              grafiek.Items.Add(gevuldItem); //Mogelijk niet nodig
            }
          }
        }
        #endregion

        #region Zoek naar de benodigde historieken en bereken de waarden die in de Dictionary moeten komen
        int sleutel = 0;
        DateTime huidigeTijd = DateTime.Now;
        foreach (GemonitordItem item in grafiekItems)
        {
          List<ItemHistoriek> historieken = new List<ItemHistoriek>();
          List<dynamic> waarden = new List<dynamic>();

          #region Zoek naar de historieken
          foreach (ItemHistoriek historiek in item.ItemHistorieken)
          {
            if ((huidigeTijd - historiek.HistoriekDatum).Days <= grafiek.Periode)
            {
              historieken.Add(historiek);
            }
          }
          #endregion

          #region Voeg de waarden van de historieken en plaats ze in de Dictionary
          GrafiekWaarde huidigeWaarde = grafiek.Waarden.ElementAt(sleutel);

          if (grafiek.Keuze == GrafiekKeuze.EvolutieAantalVermeldingen1Item)
          {
            foreach (ItemHistoriek historiek in historieken)
            {
              waarden.Add(historiek.AantalVermeldingen);
              grafiek.XLabels.Add(historiek.HistoriekDatum);
            }
            //grafiek.Type = GrafiekType.line;

            grafiek.Type = "line";
            grafiek.LegendeLijst.Add(item.Naam);
          }

          if (grafiek.Keuze == GrafiekKeuze.VergelijkingItemsDoorheenDeTijd)
          {
            foreach (ItemHistoriek historiek in historieken)
            {
              if (huidigeWaarde == GrafiekWaarde.Vermeldingen) waarden.Add(historiek.AantalVermeldingen);
              if (huidigeWaarde == GrafiekWaarde.Polariteit) waarden.Add(historiek.GemPolariteit);
              if (huidigeWaarde == GrafiekWaarde.Objectiviteit) waarden.Add(historiek.GemObjectiviteit);
              grafiek.XLabels.Add(historiek.HistoriekDatum);
            }
            grafiek.Type = "line";
            grafiek.LegendeLijst.Add(item.Naam + " - " + huidigeWaarde);
          }

          if (grafiek.Keuze == GrafiekKeuze.VergelijkingItemsOp1Moment)
          {
            foreach (ItemHistoriek historiek in historieken)
            {
              if (huidigeWaarde == GrafiekWaarde.Vermeldingen) waarden.Add(historiek.AantalVermeldingen);
              if (huidigeWaarde == GrafiekWaarde.Polariteit) waarden.Add(historiek.GemPolariteit);
              if (huidigeWaarde == GrafiekWaarde.Objectiviteit) waarden.Add(historiek.GemObjectiviteit);
              grafiek.XLabels.Add(item.Naam);
            }
            grafiek.Type = "bar";
          }

          if (grafiek.Keuze == GrafiekKeuze.KruisingTaart)
          {
            grafiek.Type = "pie";
          }

          if (grafiek.Keuze == GrafiekKeuze.KruisingBar)
          {
            grafiek.Type = "bar";
          }

          //grafiek.Data.Add(sleutel, waarden);
          //Sleutel verhogen na het toevoegen om bugs te vermijden
          ++sleutel;
          #endregion
        }
        #endregion
      }

      return grafieken;
    }

    public IEnumerable<Grafiek> GetGrafieken(int dashboardId, bool dashboard = false, bool items = false)
    {
      InitNonExistingRepo();
      return repository.ReadGrafieken(dashboard, items).Where(a => a.Dashboard != null && a.Dashboard.DashboardId.Equals(dashboardId));
    }

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
