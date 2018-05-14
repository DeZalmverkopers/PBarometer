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

        public Deelplatform ReadDeelplatform(int id)
        {
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

       

        public Settings ReadSettings()
        {
            return new Settings(ReadDeelplatform(1).OverzichtAdded, ReadDeelplatform(1).WeeklyReviewAdded, ReadDeelplatform(1).AlertsAdded);
        }

        public void UpdateOverzichtAdded(bool OverzichtAdded)
        {
            foreach (Deelplatform deelplatform in ReadDeelplatformen())
            {
                context.Entry(deelplatform).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void UpdateWeeklyReviewAdded(bool WeeklyReviewAdded)
        {
            foreach (Deelplatform deelplatform in ReadDeelplatformen())
            {
                context.Entry(deelplatform).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void UpdateAlertsAdded(bool AlertsAdded)
        {
            foreach (Deelplatform deelplatform in ReadDeelplatformen())
            {
                context.Entry(deelplatform).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        //public void UpdateSettings(bool OverzichtAdded, bool WeeklyReviewAdded, bool AlertsAdded)
        //{
        //  foreach (Deelplatform deelplatform in ReadDeelplatformen())
        //  {
        //    context.Entry(deelplatform).State = EntityState.Modified;
        //  }
        //  context.SaveChanges();
        //}

        public string ReadAchtergrondkleur()
        {
            return ReadDeelplatform(1).Achtergrondkleur;
        }

        public void UpdateAchtergrondkleur(string kleur)
        {
            foreach (Deelplatform deelplatform in ReadDeelplatformen())
            {
                context.Entry(deelplatform).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}