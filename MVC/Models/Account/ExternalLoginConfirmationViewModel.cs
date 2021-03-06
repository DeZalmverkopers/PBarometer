﻿using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }
  public class ExternalLoginListViewModel
  {
    public string ReturnUrl { get; set; }
  }
}