using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace tpr.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }

    //public class AspNetUserRoles
    //{
    //    public string UserId { get; set; }
    //    public string RoleId { get; set; }
    //} 
}