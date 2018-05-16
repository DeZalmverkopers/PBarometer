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

    public Settings GetSettings()
    {
      return repository.ReadSettings();
    }

    public void ChangeOverzichtAdded(bool OverzichtAdded)
    {
      repository.UpdateOverzichtAdded(OverzichtAdded);
    }

    public void ChangeAlertsAdded(bool AlertsAdded)
    {
      repository.UpdateAlertsAdded(AlertsAdded);
    }

    public string GetAchtergrondkleur()
    {
      return repository.ReadAchtergrondkleur();
    }

    public void ChangeAchtergrondkleur(string kleur)
    {
      repository.UpdateAchtergrondkleur(kleur);
    }

    public List<FAQItem> GetFAQItems()
    {
      return repository.ReadFAQItems();
    }

    public void AddNieuweFAQItem(FAQItem NieuweFAQItem)
    {
      repository.CreateNieuweFAQItem(NieuweFAQItem);
    }
  }
}