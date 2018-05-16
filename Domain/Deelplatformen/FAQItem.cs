using System.ComponentModel.DataAnnotations;

namespace Domain.Deelplatformen
{
  public class FAQItem
  {
    [Key]
    public string Vraag { get; set; }

    public string Antwoord { get; set; }

    public FAQItem (string Vraag, string Antwoord)
    {
      this.Vraag = Vraag;
      this.Antwoord = Antwoord;
    }
  }
}
