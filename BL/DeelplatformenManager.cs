using DAL;
using Domain.Deelplatformen;
using System.Collections.Generic;

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

    public void ChangeSettings(bool OverzichtAdded, bool WeeklyReviewAdded)
    {
      repository.UpdateSettings(OverzichtAdded, WeeklyReviewAdded);
    }
  }
}
