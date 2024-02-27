using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Boards;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Configurations
{
    public static class RoleTypeConfiguration
    {
        public static void ConfigureRoleType (this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Rolestype>(entity =>
            {
                entity.HasKey(e => e.Idroletype).HasName("rolestypes_pkey");

                entity.ToTable("rolestypes");

                entity.Property(e => e.Idroletype).HasColumnName("idroletype");
                entity.Property(e => e.Rolename)
                    .HasMaxLength(100)
                    .HasColumnName("rolename");
            });


        }
    }
}
