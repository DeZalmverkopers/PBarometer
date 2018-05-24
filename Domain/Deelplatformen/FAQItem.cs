using System.ComponentModel.DataAnnotations;

namespace Domain.Deelplatformen
{
    //Het item dat in de lijst van FAQs staat. Het bestaat uit een vraag en een antwoord.
    public class FAQItem
    {
        [Key]
        public string Vraag { get; set; }
        public string Antwoord { get; set; }
        public int DeelplatformId { get; set; }
        public Deelplatform Deelplatform { get; set; }
    }
}
