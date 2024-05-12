using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Configurations;

public static class UsersConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>(entity =>
        {
            //entity.ToTable("users");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Lastvisitdate).HasColumnName("lastvisitdate");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(250)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Registrationdate).HasColumnName("registrationdate");
            entity.Property(e => e.RefreshToken)
                        .IsRequired()
                        .HasColumnName("RefreshToken")
                        .HasColumnType("text");
            entity.Property(e => e.Roletype).HasColumnName("roletype");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.RoletypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roletype)
                .HasConstraintName("users_roletype_fkey");
        });

        //modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        //modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        //modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        //modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        //modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        //modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
    }
}
