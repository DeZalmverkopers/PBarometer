using DAL;
using Domain.Gebruikers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public class GebruikersManager
  {

    private GebruikersRepository repository;
    private UnitOfWorkManager uowManager;

    public GebruikersManager()
    {
    }

    public void AddGebruiker(Gebruiker gebruiker)
    {
      InitNonExistingRepo();
      repository.CreateGebruiker(gebruiker);
    }

    public IEnumerable<Gebruiker> GetGebruikers(bool alerts = false, bool dashboard = false)
    {
      InitNonExistingRepo();
      return repository.ReadGebruikers(alerts, dashboard);
    }

    public Gebruiker GetGebruiker(int id, bool alerts = false, bool dashboard = false)
    {
      InitNonExistingRepo();
      return repository.ReadGebruiker(id, alerts, dashboard);
    }

    public void ChangeGebruiker(Gebruiker gebruiker)
    {
      InitNonExistingRepo();
      repository.UpdateGebruiker(gebruiker);
    }

    //Commit wordt gedaan in de AlertManager tijdens het aanmaken van de Alert in de databank.
    public void ChangeGebruikerByAlert(Gebruiker gebruiker)
    {
      InitNonExistingRepo(true);
      repository.UpdateGebruiker(gebruiker);
    }

    public void RemoveGebruiker(Gebruiker gebruiker)
    {
      InitNonExistingRepo();
      repository.DeleteGebruiker(gebruiker);
    }

    public void InitNonExistingRepo(bool uow = false)
    {
      if (uow)
      {
        if (uowManager == null)
        {
          uowManager = new UnitOfWorkManager();
          repository = new GebruikersRepository(uowManager.UnitOfWork);
        }
      }
      else
      {
        repository = repository ?? new GebruikersRepository();
      }
    }
  }
}
