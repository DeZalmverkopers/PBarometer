using System.ComponentModel.DataAnnotations;

namespace MVC.Models.SuperAdmin
{
  public class MaakDeelplatformViewModel
  {
    [Required]
    public string Naam { get; set; }
    [Required]
    [Display(Name = "URL")]
    public string URLNaam { get; set; }
    [Required]
    [Display(Name = "Aantal dagen historiek")]
    [Range(0, int.MaxValue, ErrorMessage = "Het getal moet positief zijn.")]
    public int AantalDagenHistoriek { get; set; }
    [Required]
    [Display(Name = "Data ophaal frequentie (minuten)")]
    [Range(0, int.MaxValue, ErrorMessage = "Het getal moet positief zijn.")]
    public int DataOphaalFrequentie { get; set; }
    public int Id { get; set; }
  }
}