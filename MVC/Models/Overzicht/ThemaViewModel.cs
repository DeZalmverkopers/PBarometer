using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Overzicht
{
  public class ThemaViewModel
  {
    [Required]
    public string Naam { get; set; }
    [Required]
    [DataType(DataType.MultilineText)]
    public string Kernwoorden { get; set; }
    public int Id { get; set; }
  }
}