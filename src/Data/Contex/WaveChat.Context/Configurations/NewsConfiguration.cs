using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.DashBoard;

namespace WaveChat.Context.Configurations;

public static class NewsConfiguration
{
    public static void ConfigureNew(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("news_pkey");

            entity.ToTable("news");

            entity.Property(e => e.Id).HasColumnName("idnew");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Likes)
                .HasDefaultValue(0)
                .HasColumnName("likes");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });
    }
}
