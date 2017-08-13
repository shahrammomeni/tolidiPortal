using System.Data.Entity;

namespace tpr.Models
{
    public class tprGeneralDBContext : DbContext
    {

        public tprGeneralDBContext()
            : base("name=tolidiPortalGeneralCon")
        {
        }

        static tprGeneralDBContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<tprGeneralDBContext>());
        }



        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }


        //======================================================================

    }
}