using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Boards;
using WaveChat.Context.Entities.Messages;

namespace WaveChat.Context.Configurations
{
    public static class BoardConfiguration
    {
        public static void ConfigureBoard(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("boards_pkey");

                entity.ToTable("boards");

                entity.Property(e => e.Id).HasColumnName("idboard");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.Deadlinedate).HasColumnName("deadlinedate");
                entity.Property(e => e.Issuedate).HasColumnName("issuedate");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
                entity.Property(e => e.Userboard).HasColumnName("userboard");

                entity.HasOne(d => d.UserboardNavigation).WithMany(p => p.Boards)
                    .HasForeignKey(d => d.Userboard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("boards_userboard_fkey");
            });


        }
    }
}
