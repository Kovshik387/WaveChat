using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Messages;

namespace WaveChat.Context.Configurations;

public static class ChannelTypeConfiguration
{
    public static void ConfigureChannelType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Channelstype>(entity =>
        {
            entity.HasKey(e => e.Idchanneltype).HasName("channelstypes_pkey");

            entity.ToTable("channelstypes");

            entity.Property(e => e.Idchanneltype).HasColumnName("idchanneltype");
            entity.Property(e => e.Idchannel).HasColumnName("idchannel");
            entity.Property(e => e.Typename)
                .HasMaxLength(20)
                .HasColumnName("typename");

            entity.HasOne(d => d.IdchannelNavigation).WithMany(p => p.Channelstypes)
                .HasForeignKey(d => d.Idchannel)
                .HasConstraintName("channelstypes_idchannel_fkey");
        });
    }
}
