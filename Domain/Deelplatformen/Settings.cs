namespace Domain.Deelplatformen
{
  public class Settings
  {
    public bool OverzichtAdded { get; set; }
    public bool WeeklyReviewAdded { get; set; }

    public Settings(bool OverzichtAdded, bool WeeklyReviewAdded)
    {
      this.OverzichtAdded = OverzichtAdded;
      this.WeeklyReviewAdded = WeeklyReviewAdded;
    }
  }
}
