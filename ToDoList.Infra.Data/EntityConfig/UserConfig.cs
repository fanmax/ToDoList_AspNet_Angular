using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Data.EntityConfig
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(u => u.Email)
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            ToTable("AspNetUsers");
        }

    }
}
