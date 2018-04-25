﻿using Domain.Deelplatformen;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.IdentityFramework
{
  public class ApplicationUser : IdentityUser
  {

    public int? DeelplatformId { get; set; }
    public Deelplatform Deelplatform { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
  }
}
