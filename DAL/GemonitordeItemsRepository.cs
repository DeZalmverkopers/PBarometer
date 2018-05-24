using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class GemonitordeItemsRepository
    {
        private EF.DbContext context;

        public GemonitordeItemsRepository()
        {
            context = new EF.DbContext();
        }

        public GemonitordeItemsRepository(UnitOfWork uow)
        {
            context = uow.Context;
        }

        public void CreateGemonitordItem(GemonitordItem gemonitordItem)
        {
            context.GemonitordeItems.Add(gemonitordItem);
            context.SaveChanges();
        }

        public IEnumerable<GemonitordItem> ReadGemonitordeItems()
        {
            return context.GemonitordeItems.Include("DetailItems").Include("ItemHistorieken").AsEnumerable();
        }

        public void UpdateGemonitordItem(GemonitordItem gemonitordItem)
        {

            context.Entry(gemonitordItem).State = EntityState.Modified;
            context.SaveChanges();
        }

        public GemonitordItem ReadGemonitordItem(int id)
        {
            return context.GemonitordeItems.Include("DetailItems").Include("ItemHistorieken").AsEnumerable().SingleOrDefault(i => i.GemonitordItemId == id);
        }




        public void DeleteGemonitordItem(GemonitordItem gemonitordItem)
        {
            context.GemonitordeItems.Remove(gemonitordItem);
            context.SaveChanges();
        }
        public void DeleteDetailItems(DateTime limietDatum, int deelplatformId)
        {
            foreach (var detailItem in context.DetailItems)
            {
                if (detailItem.DeelplatformId == deelplatformId && detailItem.BerichtDatum < limietDatum)
                    context.DetailItems.Remove(detailItem);
            }
            context.SaveChanges();
        }
        public IEnumerable<DetailItem> ReadDetailItems()
        {
            return context.DetailItems;
        }

        public void DeleteDetailItems(IEnumerable<DetailItem> detailItems)
        {
            context.DetailItems.RemoveRange(detailItems);
            context.SaveChanges();
        }

        public Persoon ReadPersoon(int id, bool organisatie)
        {
            if (organisatie) return context.GemonitordeItems.OfType<Persoon>().Include("Organisatie").AsEnumerable().SingleOrDefault(i => i.GemonitordItemId == id) as Persoon;
            else return context.GemonitordeItems.AsEnumerable().SingleOrDefault(i => i.GemonitordItemId == id) as Persoon;
        }
        public void DeleteGemonitordeItems(IEnumerable<GemonitordItem> gemonitordeItems)
        {
            context.GemonitordeItems.RemoveRange(gemonitordeItems);
            context.SaveChanges();
        }

        public void DeleteHistorieken(List<ItemHistoriek> itemHistorieken)
        {
            context.ItemHistorieken.RemoveRange(itemHistorieken);
            context.SaveChanges();
        }
        public Persoon ReadPersoonMetHistoriekenDetailItemsOrganisaties(int id) {
            return context.GemonitordeItems.OfType<Persoon>().Include("DetailItems").Include("ItemHistorieken").Include("Organisatie").AsEnumerable().SingleOrDefault(i => i.GemonitordItemId == id) as Persoon;
        }
    }
}
