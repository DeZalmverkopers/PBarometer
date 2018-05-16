namespace Domain.Deelplatformen
{
  public class Settings
  {
    public bool OverzichtAdded { get; set; }
    public bool AlertsAdded { get; set; }

    public Settings(bool OverzichtAdded, bool AlertsAdded)
    {
      this.OverzichtAdded = OverzichtAdded;
      this.AlertsAdded = AlertsAdded;
    }
  }
}
