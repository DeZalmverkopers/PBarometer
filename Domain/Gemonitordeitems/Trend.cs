using System.ComponentModel.DataAnnotations;

namespace Domain.Gemonitordeitems
{
  public enum Trend
  {
    [Display(Name = "Dalend")]
    DOWN = 0,
    [Display(Name = "Neutraal")]
    NEUTRAL = 1,
    [Display(Name = "Stijgend")]
    UP = 2
  }
}
