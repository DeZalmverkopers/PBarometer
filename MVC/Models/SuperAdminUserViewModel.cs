using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
  public class SuperAdminUserViewModel
  {
    [Display(Name = "Admin?")]
    public bool IsAdmin { get; set; }

    [Display(Name = "Voornaam")]
    public string FirstName { get; set; }

    [Display(Name = "Achternaam")]
    public string LastName { get; set; }

    [Display(Name = "E-mailadres")]
    public string Email { get; set; }

    [Display(Name = "Rollen")]
    public ICollection<IdentityUserRole> Roles { get; set; }
  }
}