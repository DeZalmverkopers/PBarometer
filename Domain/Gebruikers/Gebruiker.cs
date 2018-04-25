using Domain.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Gebruikers
{
  public class Gebruiker
  {
    //GebruikerId: Het Id van het Gebruik.
    public int GebruikerId { get; set; }

    //Naam: De achternaam van de Gebruiker.
    //Voornaam: De voornaam van de Gebruiker.
    //Email: Het emailadres van de Gebruiker.
    //Wachtwoord: Het wachtwoord van de Gebruiker.
    //Rol: De rol van de Gebruiker.
    public String Naam { get; set; }
    public String Voornaam { get; set; }
    public String Email { get; set; }
    public String Wachtwoord { get; set; }
    public Rol Rol { get; set; }

    //Foreign keys
    //Dashboard: Het Dashboard dat bij de Gebruiker hoort.
    public Dashboard Dashboard { get; set; }
    public List<Alert> Alerts { get; set; }

  }
}
