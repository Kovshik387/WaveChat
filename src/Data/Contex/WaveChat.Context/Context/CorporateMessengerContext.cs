using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WaveChat.Context.Entities.Boards;
using WaveChat.Context.Entities.Messages;
using WaveChat.Context.Entities.DashBoard;
using WaveChat.Context.Entities.Users;
using WaveChat.Context.Entities;
using WaveChat.Context.Configurations;

namespace WaveChat.Context;

public partial class CorporateMessengerContext : DbContext
{
    public CorporateMessengerContext(DbContextOptions<CorporateMessengerContext> options)
        : base(options){ }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<Channelstype> Channelstypes { get; set; }

    public virtual DbSet<Dependency> Dependencies { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Newscomment> Newscomments { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Rolestype> Rolestypes { get; set; }

    public virtual DbSet<Statusboard> Statusboards { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userschannel> Userschannels { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBoard();
        modelBuilder.ConfigureChannel();
        modelBuilder.ConfigureDependency();
        modelBuilder.ConfigureMessage();
        modelBuilder.ConfigureNew();
        modelBuilder.ConfigureNewsComments();
        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigurePhoto();
        modelBuilder.ConfigureRoleType();
        modelBuilder.ConfigureUsersChannels();
        modelBuilder.ConfigureStatusBoard();
        modelBuilder.ConfigureChannelType();
    }
}
