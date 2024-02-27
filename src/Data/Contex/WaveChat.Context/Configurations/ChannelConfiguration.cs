using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Messages;

namespace WaveChat.Context.Configurations
{
    public static class ChannelConfiguration
    {
        public static void ConfigureChannel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasKey(e => e.Idchannel).HasName("channels_pkey");

                entity.ToTable("channels");

                entity.Property(e => e.Idchannel).HasColumnName("idchannel");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

        }
    }
}
