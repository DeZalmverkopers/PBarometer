﻿using Domain.IdentityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BL.IdentityFramework
{
  // Configure the application sign-in manager which is used in this application.
  //ApplicationSignInManager: functionaliteit om User in te loggen.
  public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
  {
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
    {
    }

    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    {
      return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    }

    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    {
      return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
  }
}
