using DAL.EF;
using Domain.Gemonitordeitems;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public IEnumerable<GemonitordItem> ReadGemonitordeItems(bool grafieken = false)
    {
      if (!grafieken) return context.GemonitordeItems.Include("DetailItems").AsEnumerable();
      else return context.GemonitordeItems.Include("Grafieken").Include("DetailItems").AsEnumerable();
    }

    public GemonitordItem ReadGemonitordItem(int id, bool grafieken = false)
    {
      if (!grafieken) return context.GemonitordeItems.Include("DetailItems").AsEnumerable().SingleOrDefault(i => i.GemonitordItemId == id);
      else return context.GemonitordeItems.Include("Grafieken").Include("DetailItems").AsEnumerable().SingleOrDefault(i => i.GemonitordItemId == id);
    }

    public void UpdateGemonitordItem(GemonitordItem gemonitordItem)
    {
      context.Entry(gemonitordItem).State = EntityState.Modified;
      context.SaveChanges();
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
  }
}
