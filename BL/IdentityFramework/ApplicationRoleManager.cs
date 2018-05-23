using DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.IdentityFramework
{
  public class ApplicationRoleManager : RoleManager<IdentityRole>
  {

    public ApplicationRoleManager()
      : base(new RoleStore<IdentityRole>(new DbContext()))
    {
    }

    public static ApplicationRoleManager Create()
    {
      return new ApplicationRoleManager();
    }

    public void TestMethode()
    {

    }
  }
}