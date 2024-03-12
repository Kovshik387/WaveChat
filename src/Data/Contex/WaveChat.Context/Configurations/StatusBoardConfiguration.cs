using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Boards;

namespace WaveChat.Context.Configurations;

public static class StatusBoardConfiguration
{
    public static void ConfigureStatusBoard(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Statusboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statusboards_pkey");

            entity.ToTable("statusboards");

            entity.Property(e => e.Id).HasColumnName("idstatusboard");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });
    }
}
