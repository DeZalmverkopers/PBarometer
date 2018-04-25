using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
  public class SuperAdminUserViewModel
  {
    [Display(Name = "E-mailadres")]
    public string Email { get; set; }
  }
}