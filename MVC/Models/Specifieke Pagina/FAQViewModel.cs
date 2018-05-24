using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Specifieke_Pagina
{
  public class FAQViewModel
  {
    [Key]
    public string Vraag { get; set; }
    public string Antwoord { get; set; }
  }
}