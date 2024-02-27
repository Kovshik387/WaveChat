using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities;

namespace WaveChat.Context.Configurations;

public static class PhotoConfiguration
{
    public static void ConfigurePhoto(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Idphoto).HasName("photos_pkey");

            entity.ToTable("photos");

            entity.Property(e => e.Idphoto).HasColumnName("idphoto");
            entity.Property(e => e.Bucket)
                .HasMaxLength(100)
                .HasColumnName("bucket");
            entity.Property(e => e.Idboard).HasColumnName("idboard");
            entity.Property(e => e.Idchannel).HasColumnName("idchannel");
            entity.Property(e => e.Idmessage).HasColumnName("idmessage");
            entity.Property(e => e.Idnew).HasColumnName("idnew");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");

            entity.HasOne(d => d.IdboardNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Idboard)
                .HasConstraintName("photos_idboard_fkey");

            entity.HasOne(d => d.IdchannelNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Idchannel)
                .HasConstraintName("photos_idchannel_fkey");

            entity.HasOne(d => d.IdmessageNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Idmessage)
                .HasConstraintName("photos_idmessage_fkey");

            entity.HasOne(d => d.IdnewNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Idnew)
                .HasConstraintName("photos_idnew_fkey");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("photos_iduser_fkey");
        });
    }
}
