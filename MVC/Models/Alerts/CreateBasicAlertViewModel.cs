using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Alerts
{
  public class CreateBasicAlertViewModel
  {
    public int Id { get; set; }
    [Required]
    public string Beschrijving { get; set; }
    public string Onderwerp { get; set; }
    public string Eigenschap { get; set; }
    public string Trend { get; set; }
    public bool Mail { get; set; }
    public bool Mobiel { get; set; }
  }
}