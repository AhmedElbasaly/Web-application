using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            creatDefaultRolesAndUser();
        }
        public void creatDefaultRolesAndUser()
        {
            var roleMangar = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleMangar.RoleExists("Admins"))
            {
                role.Name = "Admins";
                roleMangar.Create(role);

                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ahmed";
                user.Email = "Ahmedsaid222@yhoo.com";
                var Check = UserManager.Create(user, "ahmed12345");
                if (Check.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admins");

                }
            }
        }
    }
}
