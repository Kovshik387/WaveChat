using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Messages;

namespace WaveChat.Context.Configurations;

public static class MessageConfiguration
{
    public static void ConfigureMessage(this ModelBuilder modelBuilder) {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("message_pkey");

            entity.ToTable("message");

            entity.Property(e => e.Id).HasColumnName("idmessage");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Idchannel).HasColumnName("idchannel");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Isread)
                .HasDefaultValue(false)
                .HasColumnName("isread");
            entity.Property(e => e.Senddate).HasColumnName("senddate");

            entity.HasOne(d => d.IdchannelNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Idchannel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("message_idchannel_fkey");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("message_iduser_fkey");
        });
    }
}
