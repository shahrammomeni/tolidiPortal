using System.Data.Entity;

namespace tpr.Models
{
    public class tprDBContext : DbContext
    {

        public tprDBContext()
            : base("name=tolidiPortalCon")
        {
        }

        public tprDBContext( string conStr)
            : base(conStr)
        {
        }

        static tprDBContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<tprDBContext>());
        }

        //public virtual DbSet<vu_UserDetail> vu_UserDetails { get; set; }
     
     
        //===============================SP=====================================

        //==============================FN==========================================

    }
}