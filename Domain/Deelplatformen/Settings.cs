namespace Domain.Deelplatformen
{
  //De instellingen van wat de niet-ingelogde gebruikers al dan niet kunnen raadplegen op de site.
  public class Settings
  {
    //De instelling of het overzicht al dan niet raadpleegbaar is door niet-ingelogde gebruikers.
    public bool OverzichtAdded { get; set; }

    //De instelling of de alerts al dan niet raadpleegbaar is door niet-ingelogde gebruikers.
    public bool AlertsAdded { get; set; }

    public Settings(bool OverzichtAdded, bool AlertsAdded)
    {
      this.OverzichtAdded = OverzichtAdded;
      this.AlertsAdded = AlertsAdded;
    }
  }
}
