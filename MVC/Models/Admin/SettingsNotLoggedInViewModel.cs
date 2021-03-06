﻿using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
  public class SettingsNotLoggedInViewModel
  {
    [Display(Name = "Overzicht")]
    public bool OverzichtAdded { get; set; }

    [Display(Name = "Alerts")]
    public bool AlertsAdded { get; set; }
  }
}