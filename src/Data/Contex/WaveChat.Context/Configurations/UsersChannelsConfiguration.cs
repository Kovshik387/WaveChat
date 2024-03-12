using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Messages;

namespace WaveChat.Context.Configurations;

public static class UsersChannelsConfiguration
{
    public static void ConfigureUsersChannels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Userschannel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userschannels_pkey");

            entity.ToTable("userschannels");

            entity.Property(e => e.Id).HasColumnName("iduserchannel");
            entity.Property(e => e.Channelid).HasColumnName("channelid");

            entity.HasOne(d => d.Channel).WithMany(p => p.Userschannels)
                .HasForeignKey(d => d.Channelid)
                .HasConstraintName("userschannels_channelid_fkey");
        });
    }
}
