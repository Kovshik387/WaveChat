using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.DashBoard;

namespace WaveChat.Context.Configurations;

public static class NewsCommentsConfiguration
{
    public static void ConfigureNewsComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Newscomment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("newscomments_pkey");

            entity.ToTable("newscomments");

            entity.Property(e => e.Id).HasColumnName("idcomment");
            entity.Property(e => e.Commentdate).HasColumnName("commentdate");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Idnew).HasColumnName("idnew");
            entity.Property(e => e.Iduser).HasColumnName("iduser");

            entity.HasOne(d => d.IdnewNavigation).WithMany(p => p.Newscomments)
                .HasForeignKey(d => d.Idnew)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("newscomments_idnew_fkey");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Newscomments)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("newscomments_iduser_fkey");
        });
    }
}
