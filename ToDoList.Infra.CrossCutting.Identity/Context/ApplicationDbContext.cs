using ToDoList.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ToDoList.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
