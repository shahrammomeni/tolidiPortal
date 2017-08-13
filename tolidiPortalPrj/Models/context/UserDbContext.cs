using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace tpr.Models
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext()
            : base("name=membership")
        {

        }

        static UserDbContext()
        {
            //var AutomaticMigrationsEnabled = bool.Parse(WebConfigurationManager.AppSettings["AutomaticMigrationsEnabled"]);
            //if(AutomaticMigrationsEnabled)
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserDbContext, Migrations.Configuration>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<UserDbContext>());
        }

    }
}