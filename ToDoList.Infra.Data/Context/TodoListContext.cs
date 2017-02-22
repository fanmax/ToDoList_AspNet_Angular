using System.Data.Entity;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.EntityConfig;

namespace ToDoList.Infra.Data.Context
{
    public class TodoListContext: DbContext
    {

        public TodoListContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<TodoListContext>(null);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assigments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());
            base.OnModelCreating(modelBuilder);            
        }

        
    }
}
