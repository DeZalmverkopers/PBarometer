using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Deelplatformen;
using System.Data.Entity;

namespace DAL
{
  public class DeelplatformenRepository
  {
    private EF.DbContext context;

    public DeelplatformenRepository()
    {
      context = new EF.DbContext();
    }
    public DeelplatformenRepository(UnitOfWork uow)
    {
      context = uow.Context;
    }

    public void CreateDeelplatform(Deelplatform deelplatform)
    {
      context.Deelplatformen.Add(deelplatform);
      context.SaveChanges();
    }

    public IEnumerable<Deelplatform> ReadDeelplatformen()
    {
      return context.Deelplatformen;
    }

    public Deelplatform ReadDeelplatform(int id, bool relationeleEntiteiten = false)
    {
      if (relationeleEntiteiten)
      {
        context.Deelplatformen.Include("Alerts").Include("GemonitordeItems").Include("Dashboards").Include("DetailItems").FirstOrDefault();
      }
      return context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id);
    }

    public Deelplatform ReadDeelplatformByName(string naam)
    {
      return context.Deelplatformen.FirstOrDefault(b => b.Naam.Equals(naam, StringComparison.OrdinalIgnoreCase));
    }

    public void UpdateDeelplatform(Deelplatform deelplatform)
    {
      context.Entry(deelplatform).State = EntityState.Modified;
      context.SaveChanges();
    }

    public void DeleteDeelplatform(Deelplatform deelplatform)
    {
      context.Deelplatformen.Remove(deelplatform);
      context.SaveChanges();
    }

    //Haalt uit de databank de instellingen over wat de niet-ingelogde gebruikers kunnen
    //zien en doen in het huidige deelplatform.
    public Settings ReadSettings(int id)
    {
      Deelplatform deelplatform = context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id);
      return new Settings(deelplatform.OverzichtAdded, deelplatform.AlertsAdded);
    }

    //Update of de niet-ingelogde gebruikers al dan niet het overzicht kunnen raadplegen.
    public void UpdateOverzichtAdded(int id, bool OverzichtAdded)
    {
      context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id).OverzichtAdded = OverzichtAdded;
      context.SaveChanges();
    }

    //Update of de niet-ingelogde gebruikers al dan niet de alerts kunnen raadplegen.
    public void UpdateAlertsAdded(int id, bool AlertsAdded)
    {
      context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id).AlertsAdded = AlertsAdded;
      context.SaveChanges();
    }

    public string ReadAchtergrondkleur(int id)
    {
      return context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id).Achtergrondkleur;
    }

    public void UpdateAchtergrondkleur(int id, string kleur)
    {
      context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id).Achtergrondkleur = kleur;
      context.SaveChanges();
    }

    public List<FAQItem> ReadFAQItems(int id)
    {
      return context.Deelplatformen.Include("FAQItems").FirstOrDefault(b => b.DeelplatformId == id).FAQItems;
    }

    public void CreateNieuweFAQItem(int id, FAQItem NieuweFAQItem)
    {
      context.Deelplatformen.FirstOrDefault(b => b.DeelplatformId == id).FAQItems.Add(NieuweFAQItem);
      context.SaveChanges();
    }

    public void DeleteFAQItem(int id, string vraag)
    {
      Deelplatform deelplatform = context.Deelplatformen.Include("FAQItems").FirstOrDefault(b => b.DeelplatformId == id);
      context.FAQItems.Remove(deelplatform.FAQItems.FirstOrDefault(faqitem => faqitem.Vraag.Equals(vraag)));
      context.SaveChanges();
    }
  }
}