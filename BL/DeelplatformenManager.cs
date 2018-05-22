using DAL;
using Domain.Deelplatformen;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BL
{
  public class DeelplatformenManager
  {
    private readonly DeelplatformenRepository repository;

    public DeelplatformenManager()
    {
      repository = new DeelplatformenRepository();
    }

    public void AddDeelplatform(Deelplatform deelplatform)
    {
      repository.CreateDeelplatform(deelplatform);
    }

    public IEnumerable<Deelplatform> GetDeelplatformen()
    {
      return repository.ReadDeelplatformen();
    }

    public Deelplatform GetDeelplatformByName(string naam)
    {
      return repository.ReadDeelplatformByName(naam);
    }
    public Deelplatform GetDeelplatform(int id)
    {
      return repository.ReadDeelplatform(id);
    }
    public void ChangeDeelplatform(Deelplatform deelplatform)
    {
      repository.UpdateDeelplatform(deelplatform);
    }

    public void RemoveDeelplatform(Deelplatform deelplatform)
    {
      repository.DeleteDeelplatform(deelplatform);
    }

    public void RemoveDeelplatform(int id)
    {
      repository.DeleteDeelplatform(repository.ReadDeelplatform(id));
    }

    public Deelplatform GetDeelplatformByURL(string url)
    {
      return repository.ReadDeelplatformen().FirstOrDefault(a => a.URLnaam.Equals(url, StringComparison.OrdinalIgnoreCase));
    }

    //Haalt op wat niet-ingelogde gebruikers kunnen zien en doen op de site.
    public Settings GetSettings(int id)
    {
      return repository.ReadSettings(id);
    }

    //Verandert of de niet-ingelogde gebruikers al dan niet het overzicht kunnen raadplegen.
    public void ChangeOverzichtAdded(int id, bool OverzichtAdded)
    {
      repository.UpdateOverzichtAdded(id, OverzichtAdded);
    }

    //Verandert of de niet-ingelogde gebruikers al dan niet de alerts kunnen raadplegen.
    public void ChangeAlertsAdded(int id, bool AlertsAdded)
    {
      repository.UpdateAlertsAdded(id, AlertsAdded);
    }

    public string GetAchtergrondkleur(int id)
    {
      return repository.ReadAchtergrondkleur(id);
    }

    public void ChangeAchtergrondkleur(int id, string kleur)
    {
      repository.UpdateAchtergrondkleur(id, kleur);
    }

    public List<FAQItem> GetFAQItems(int id)
    {
      return repository.ReadFAQItems(id);
    }

    public void AddNieuweFAQItem(int id, FAQItem NieuweFAQItem)
    {
      repository.CreateNieuweFAQItem(id, NieuweFAQItem);
    }

    public void RemoveFAQItem(int id, string vraag)
    {
      repository.DeleteFAQItem(id, vraag);
    }
  }
}