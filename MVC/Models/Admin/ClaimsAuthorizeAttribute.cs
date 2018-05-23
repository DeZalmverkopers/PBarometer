﻿using System.Security.Claims;
using System.Web.Mvc;

namespace MVC.Models.Admin
{
  public class ClaimsAuthorizeAttribute : AuthorizeAttribute
  {
    private string claimType;
    private string claimValue;
    public ClaimsAuthorizeAttribute(string type, string value)
    {
      claimType = type;
      this.claimValue = value;
    }
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
      var user = filterContext.HttpContext.User as ClaimsPrincipal;
      if (user != null && user.HasClaim(claimType, claimValue))
      {
        base.OnAuthorization(filterContext);
      }
      else
      {
        base.HandleUnauthorizedRequest(filterContext);
      }
    }
  }
}