﻿using DAL;
using Domain.Dashboards;
using Domain.Gebruikers;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public class AlertManager
  {
    private AlertRepository repository;
    private GemonitordeItemsManager gemonitordItemsManager;
    private GebruikersManager gebruikersManager;
    private UnitOfWorkManager uowManager;

    public AlertManager()
    {
    }

    public void AddAlert(Alert alert)
    {
      InitNonExistingRepo(true);
      repository.CreateAlert(alert);
      //repository.CreateAlert(alert);

      uowManager.Save();
    }

    public IEnumerable<Alert> GetAlerts(bool gebruiker = false, bool gemonitordItem = false)
    {
      InitNonExistingRepo();
      return repository.ReadAlerts(gebruiker, gemonitordItem);
    }

    public Alert GetAlert(int id, bool gebruiker = false, bool gemonitordItem = false)
    {
      InitNonExistingRepo();
      return repository.ReadAlert(id, gebruiker, gemonitordItem);
    }

    public void ChangeAlert(Alert alert)
    {
      InitNonExistingRepo();
      repository.UpdateAlert(alert);
    }

    public void RemoveAlertVanGebruiker(Alert alert)
    {
      InitNonExistingRepo();
      repository.DeleteAlert(alert);
    }

    //GenereerAlerts: De Alerts worden allemaal nagekeken en getriggerd wanneer nodig.
    public void GenereerAlerts()
    {
      List<Alert> alerts = GetAlerts(false, true).ToList();

      foreach (var alert in alerts)
        if (alert.Geactiveerd)
        {
          StringBuilder bob = new StringBuilder();
          string vergelijkingDaling = BerekenDalingEnVergelijk(alert, alert.GemonitordItem);
          string vergelijkingStijging = BerekenStijgingEnVergelijk(alert, alert.GemonitordItem);
          string belangrijkheid = BerekenBelangrijkheid(alert, alert.GemonitordItem);
          string polariteit = BerekenPolariteit(alert, alert.GemonitordItem);
          string objectiviteit = BerekenObjectiviteit(alert, alert.GemonitordItem);
          bob.Append(vergelijkingDaling).Append(vergelijkingStijging).Append(belangrijkheid).Append(polariteit).Append(objectiviteit);
          alert.TriggerRedenen = bob.ToString();
          if (alert.Mail)
          {
            SendMail(alert);
          }
          ChangeAlert(alert);
        }
    }


    private string BerekenDalingEnVergelijk(Alert alert, GemonitordItem item)
    {
      //Bereken stijging of daling aan de hand van de ingestelde periode vergeleken met de vorige periode.
      List<ItemHistoriek> historieken = item.ItemHistorieken;
      List<ItemHistoriek> vorigeHistorieken = new List<ItemHistoriek>();
      List<ItemHistoriek> relevanteHistorieken = new List<ItemHistoriek>();

      //Zoek de relevante historieken.
      foreach (var historiek in historieken)
      {
        DateTime currentTime = DateTime.Now;
        if ((currentTime - historiek.SynchronisatieDatum).Days <= alert.MinStijgingPeriode)
        {
          relevanteHistorieken.Add(historiek);
        }

        if ((currentTime - historiek.SynchronisatieDatum).Days >= alert.MinStijgingPeriode
          && (currentTime - historiek.SynchronisatieDatum).Days < alert.MinStijgingPeriode * 2)
        {
          vorigeHistorieken.Add(historiek);
        }
      }

      //Bereken de daling aan de hand van de relevante historieken en trigger de Alert wanneer nodig.
      if (vorigeHistorieken.Count != 0)
      {
        double huidigPercentage = ((relevanteHistorieken.Last().AantalVermeldingen / relevanteHistorieken.First().AantalVermeldingen) * 100) - 100;
        double vorigePercentage = ((vorigeHistorieken.Last().AantalVermeldingen / vorigeHistorieken.First().AantalVermeldingen) * 100) - 100;
        if (huidigPercentage > vorigePercentage)
        {
          double verschil = huidigPercentage - vorigePercentage;
          if (verschil > alert.MinStijging)
          {
            alert.Triggered = true;
            return "Daling: Er waren opmerkelijk genoeg tijdens deze periode " + Math.Round(verschil, 2) + "% meer vermeldingen in verband met " + item.Naam + ".\n";
          }
          return "Daling: We detecteren geen belangrijke trends.\n";
        }
      }
      return "Daling: Niet genoeg data!\n";
    }

    private string BerekenStijgingEnVergelijk(Alert alert, GemonitordItem item)
    {
      //Bereken stijging of daling aan de hand van de ingestelde periode vergeleken met de vorige periode.
      List<ItemHistoriek> historieken = item.ItemHistorieken;
      List<ItemHistoriek> vorigeHistorieken = new List<ItemHistoriek>();
      List<ItemHistoriek> relevanteHistorieken = new List<ItemHistoriek>();

      //Zoek de relevante historieken.
      foreach (var historiek in historieken)
      {
        DateTime currentTime = DateTime.Now;
        if ((currentTime - historiek.SynchronisatieDatum).Days <= alert.MinDalingPeriode)
        {
          relevanteHistorieken.Add(historiek);
        }

        if ((currentTime - historiek.SynchronisatieDatum).Days >= alert.MinDalingPeriode
          && (currentTime - historiek.SynchronisatieDatum).Days < alert.MinDalingPeriode * 2)
        {
          vorigeHistorieken.Add(historiek);
        }
      }

      //Bereken de daling aan de hand van de relevante historieken en trigger de Alert wanneer nodig.
      if (vorigeHistorieken.Count > 1)
      {
        double huidigPercentage = ((relevanteHistorieken.Last().AantalVermeldingen / relevanteHistorieken.First().AantalVermeldingen) * 100) - 100;
        double vorigePercentage = ((vorigeHistorieken.Last().AantalVermeldingen / vorigeHistorieken.First().AantalVermeldingen) * 100) - 100;
        if (huidigPercentage < vorigePercentage)
        {
          double verschil = vorigePercentage - huidigPercentage;
          if (verschil > alert.MinDaling)
          {
            alert.Triggered = true;
            return "Stijging: Er waren opmerkelijk genoeg tijdens deze periode " + Math.Round(verschil, 2) + "% minder vermeldingen in verband met " + item.Naam + ".\n";
          }
          return "Stijging: We detecteren geen belangrijke trends.\n";
        }
      }
      return "Stijging: Niet genoeg data!\n";
    }

    private string BerekenBelangrijkheid(Alert alert, GemonitordItem item)
    {
      //Bereken stijging in vermeldingen aan de hand van de ingestelde periode.
      List<ItemHistoriek> historieken = item.ItemHistorieken;
      List<ItemHistoriek> relevanteHistorieken = new List<ItemHistoriek>();

      //Zoek de relevante historieken.
      foreach (var historiek in historieken)
      {
        if ((DateTime.Now - historiek.SynchronisatieDatum).Days <= alert.BelangrijkheidsPeriode)
        {
          relevanteHistorieken.Add(historiek);
        }
      }

      //Bereken de stijging in vermeldingen aan de hand van de relevante historieken en trigger de Alert wanneer nodig.
      if (relevanteHistorieken.Count > 1)
      {
        double verhoging = relevanteHistorieken.Last().AantalVermeldingen - relevanteHistorieken.First().AantalVermeldingen;
        if (verhoging >= alert.BelangrijkWaarde)
        {
          alert.Triggered = true;
          return "Vermeldingen: Het aantal vermeldingen in verband met " + item.Naam + " is met een aanzienlijk aantal van " + Math.Round(verhoging, 2) + " vermeldingen op " + alert.BelangrijkheidsPeriode + " dagen gestegen. Dit onderwerp zou weleens belangrijk kunnen zijn.\n";
        }
        else if (verhoging <= alert.NietBelangrijkWaarde)
        {
          alert.Triggered = true;
          return "Vermeldingen: Het aantal vermeldingen in verband met " + item.Naam + " is met een laag aantal van " + Math.Round(verhoging, 2) + " vermeldingen op " + alert.BelangrijkheidsPeriode + " dagen gestegen. De belangrijkheid is in twijfel te trekken.\n";
        }
        return "Vermeldingen: We detecteren geen belangrijke trends.\n";
      }
      return "Vermeldingen: Niet genoeg data!\n";
    }

    private string BerekenPolariteit(Alert alert, GemonitordItem item)
    {
      //Bereken de polariteit aan de hand van de ingestelde periode.
      List<ItemHistoriek> historieken = item.ItemHistorieken;
      List<ItemHistoriek> relevanteHistorieken = new List<ItemHistoriek>();

      //Zoek de relevante historieken.
      foreach (var historiek in historieken)
      {
        if ((DateTime.Now - historiek.SynchronisatieDatum).Days <= alert.PolariteitsPeriode)
        {
          relevanteHistorieken.Add(historiek);
        }
      }

      //Bereken de polariteit aan de hand van de relevante historieken en trigger de Alert wanneer nodig.
      double gemiddeldePolariteit = 0;
      foreach (var historiek in relevanteHistorieken)
      {
        gemiddeldePolariteit += historiek.GemPolariteit;
      }
      gemiddeldePolariteit = gemiddeldePolariteit / relevanteHistorieken.Count();
      if (gemiddeldePolariteit <= alert.MinPolariteit)
      {
        alert.Triggered = true;
        return "Polariteit: Het onderwerp " + item.Naam + " is de voorbije " + alert.PolariteitsPeriode + " dagen nogal negatief (Waarde: " + Math.Round(gemiddeldePolariteit, 2) + ") in de media gekomen.\n";
      }
      else if (gemiddeldePolariteit >= alert.MaxPolariteit)
      {
        alert.Triggered = true;
        return "Polariteit: Het onderwerp " + item.Naam + " is de voorbije " + alert.PolariteitsPeriode + " dagen vrij positief (Waarde: " + Math.Round(gemiddeldePolariteit, 2) + ") in de media gekomen.\n";
      }
      return "Polariteit: We detecteren geen belangrijke trends.\n";
    }

    private string BerekenObjectiviteit(Alert alert, GemonitordItem item)
    {
      //Bereken de objectiviteit aan de hand van de ingestelde periode.
      List<ItemHistoriek> historieken = item.ItemHistorieken;
      List<ItemHistoriek> relevanteHistorieken = new List<ItemHistoriek>();

      //Zoek de relevante historieken.
      foreach (var historiek in historieken)
      {
        if ((DateTime.Now - historiek.SynchronisatieDatum).Days <= alert.ObjectiviteitsPeriode)
        {
          relevanteHistorieken.Add(historiek);
        }
      }

      //Bereken de objectiviteit aan de hand van de relevante historieken en trigger de Alert wanneer nodig.
      double gemiddeldeObjectiviteit = 0;
      foreach (var historiek in relevanteHistorieken)
      {
        gemiddeldeObjectiviteit += historiek.GemObjectiviteit;
      }
      gemiddeldeObjectiviteit = gemiddeldeObjectiviteit / relevanteHistorieken.Count();
      if (gemiddeldeObjectiviteit <= alert.MinObjectiviteit)
      {
        alert.Triggered = true;
        return "Objectiviteit: Het onderwerp " + item.Naam + " is de voorbije " + alert.ObjectiviteitsPeriode + " dagen heel meningrijk (Waarde: " + Math.Round(gemiddeldeObjectiviteit, 2) + ") op sociale media besproken.\n";
      }
      else if (gemiddeldeObjectiviteit >= alert.MaxObjectiviteit)
      {
        alert.Triggered = true;
        return "Objectiviteit: Het onderwerp " + item.Naam + " is de voorbije " + alert.ObjectiviteitsPeriode + " dagen heel feitelijk (Waarde: " + Math.Round(gemiddeldeObjectiviteit, 2) + ") op sociale media besproken.\n";
      }
      return "Objectiviteit: We detecteren geen belangrijke trends.\n";
    }

    private void SendMail(Alert alert)
    {
      //Verstuur mail
    }

    public void InitNonExistingRepo(bool uow = false)
    {
      if (uow)
      {
        if (uowManager == null)
        {
          uowManager = new UnitOfWorkManager();
          repository = new AlertRepository(uowManager.UnitOfWork);
        }
      }
      else
      {
        repository = repository ?? new AlertRepository();
      }
    }
  }
}