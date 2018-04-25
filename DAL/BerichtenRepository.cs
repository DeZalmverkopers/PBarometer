using DAL.EF;
using Domain.Bericht;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class BerichtenRepository
  {
    private EF.DbContext context;

    public BerichtenRepository()
    {
            context = new EF.DbContext();
    }
    public BerichtenRepository(UnitOfWork uow)
    {
      context = uow.Context;
    }

    public void CreateBericht(Bericht bericht)
    {
      context.Berichten.Add(bericht);
      context.SaveChanges();
    }
    public void CreateBerichten(IEnumerable<Bericht> berichten)
    {
      foreach (var bericht in berichten)
      {
        context.Berichten.Add(bericht);
      }
      context.SaveChanges();
    }

    public IEnumerable<Bericht> ReadBerichten()
    {
      return context.Berichten;
    }

    public Bericht ReadBericht(int id)
    {
      return context.Berichten.SingleOrDefault(b => b.BerichtId == id);
    }

    public void UpdateBericht(Bericht bericht)
    {
      context.Entry(bericht).State = EntityState.Modified;
      context.SaveChanges();
    }

    public void DeleteBericht(Bericht bericht)
    {
      context.Berichten.Remove(bericht);
      context.SaveChanges();
    }
  }
}
