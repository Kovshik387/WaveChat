﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WaveChat.Context;

#nullable disable

namespace WaveChat.Context.Migrations.PgSql.Migrations
{
    [DbContext(typeof(CorporateMessengerContext))]
    [Migration("20240512211452_init1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WaveChat.Context.Entities.Boards.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idboard");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateOnly>("Deadlinedate")
                        .HasColumnType("date")
                        .HasColumnName("deadlinedate");

                    b.Property<DateOnly>("Issuedate")
                        .HasColumnType("date")
                        .HasColumnName("issuedate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<int>("Userboard")
                        .HasColumnType("integer")
                        .HasColumnName("userboard");

                    b.HasKey("Id")
                        .HasName("boards_pkey");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.HasIndex("Userboard");

                    b.ToTable("boards", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Boards.Dependency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("iddependency");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Idboard")
                        .HasColumnType("integer")
                        .HasColumnName("idboard");

                    b.Property<int>("Iduser")
                        .HasColumnType("integer")
                        .HasColumnName("iduser");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("dependencies_pkey");

                    b.HasIndex("Idboard");

                    b.HasIndex("Iduser");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("dependencies", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Boards.Statusboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idstatusboard");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("statusboards_pkey");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("statusboards", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.DashBoard.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idnew");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<int>("Likes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("likes");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("title");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("news_pkey");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("news", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.DashBoard.Newscomment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idcomment");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Commentdate")
                        .HasColumnType("date")
                        .HasColumnName("commentdate");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<int>("Idnew")
                        .HasColumnType("integer")
                        .HasColumnName("idnew");

                    b.Property<int>("Iduser")
                        .HasColumnType("integer")
                        .HasColumnName("iduser");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("newscomments_pkey");

                    b.HasIndex("Idnew");

                    b.HasIndex("Iduser");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("newscomments", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idchannel");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("channels_pkey");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("channels", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Channelstype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idchanneltype");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Idchannel")
                        .HasColumnType("integer")
                        .HasColumnName("idchannel");

                    b.Property<string>("Typename")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("typename");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("channelstypes_pkey");

                    b.HasIndex("Idchannel");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("channelstypes", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idmessage");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<int>("Idchannel")
                        .HasColumnType("integer")
                        .HasColumnName("idchannel");

                    b.Property<int>("Iduser")
                        .HasColumnType("integer")
                        .HasColumnName("iduser");

                    b.Property<bool>("Isread")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("isread");

                    b.Property<DateOnly>("Senddate")
                        .HasColumnType("date")
                        .HasColumnName("senddate");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("message_pkey");

                    b.HasIndex("Idchannel");

                    b.HasIndex("Iduser");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("message", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Userschannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("iduserchannel");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Channelid")
                        .HasColumnType("integer")
                        .HasColumnName("channelid");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("userschannels_pkey");

                    b.HasIndex("Channelid");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("userschannels", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idphoto");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bucket")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("bucket");

                    b.Property<int?>("Idboard")
                        .HasColumnType("integer")
                        .HasColumnName("idboard");

                    b.Property<int?>("Idchannel")
                        .HasColumnType("integer")
                        .HasColumnName("idchannel");

                    b.Property<int?>("Idmessage")
                        .HasColumnType("integer")
                        .HasColumnName("idmessage");

                    b.Property<int?>("Idnew")
                        .HasColumnType("integer")
                        .HasColumnName("idnew");

                    b.Property<int>("Iduser")
                        .HasColumnType("integer")
                        .HasColumnName("iduser");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("image");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("photos_pkey");

                    b.HasIndex("Idboard");

                    b.HasIndex("Idchannel");

                    b.HasIndex("Idmessage");

                    b.HasIndex("Idnew");

                    b.HasIndex("Iduser");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("photos", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Users.Rolestype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idroletype");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Rolename")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("rolename");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("rolestypes_pkey");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("rolestypes", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("Lastname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("lastname");

                    b.Property<DateOnly>("Lastvisitdate")
                        .HasColumnType("date")
                        .HasColumnName("lastvisitdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Passwordhash")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("passwordhash");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("Registrationdate")
                        .HasColumnType("date")
                        .HasColumnName("registrationdate");

                    b.Property<int?>("Roletype")
                        .HasColumnType("integer")
                        .HasColumnName("roletype");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("surname");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Roletype");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Boards.Board", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Users.User", "UserboardNavigation")
                        .WithMany("Boards")
                        .HasForeignKey("Userboard")
                        .IsRequired()
                        .HasConstraintName("boards_userboard_fkey");

                    b.Navigation("UserboardNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Boards.Dependency", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Boards.Board", "IdboardNavigation")
                        .WithMany("Dependencies")
                        .HasForeignKey("Idboard")
                        .IsRequired()
                        .HasConstraintName("dependencies_idboard_fkey");

                    b.HasOne("WaveChat.Context.Entities.Users.User", "IduserNavigation")
                        .WithMany("Dependencies")
                        .HasForeignKey("Iduser")
                        .IsRequired()
                        .HasConstraintName("dependencies_iduser_fkey");

                    b.Navigation("IdboardNavigation");

                    b.Navigation("IduserNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.DashBoard.Newscomment", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.DashBoard.News", "IdnewNavigation")
                        .WithMany("Newscomments")
                        .HasForeignKey("Idnew")
                        .IsRequired()
                        .HasConstraintName("newscomments_idnew_fkey");

                    b.HasOne("WaveChat.Context.Entities.Users.User", "IduserNavigation")
                        .WithMany("Newscomments")
                        .HasForeignKey("Iduser")
                        .IsRequired()
                        .HasConstraintName("newscomments_iduser_fkey");

                    b.Navigation("IdnewNavigation");

                    b.Navigation("IduserNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Channelstype", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Messages.Channel", "IdchannelNavigation")
                        .WithMany("Channelstypes")
                        .HasForeignKey("Idchannel")
                        .HasConstraintName("channelstypes_idchannel_fkey");

                    b.Navigation("IdchannelNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Message", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Messages.Channel", "IdchannelNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("Idchannel")
                        .IsRequired()
                        .HasConstraintName("message_idchannel_fkey");

                    b.HasOne("WaveChat.Context.Entities.Users.User", "IduserNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("Iduser")
                        .IsRequired()
                        .HasConstraintName("message_iduser_fkey");

                    b.Navigation("IdchannelNavigation");

                    b.Navigation("IduserNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Userschannel", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Messages.Channel", "Channel")
                        .WithMany("Userschannels")
                        .HasForeignKey("Channelid")
                        .HasConstraintName("userschannels_channelid_fkey");

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Photo", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Boards.Board", "IdboardNavigation")
                        .WithMany("Photos")
                        .HasForeignKey("Idboard")
                        .HasConstraintName("photos_idboard_fkey");

                    b.HasOne("WaveChat.Context.Entities.Messages.Channel", "IdchannelNavigation")
                        .WithMany("Photos")
                        .HasForeignKey("Idchannel")
                        .HasConstraintName("photos_idchannel_fkey");

                    b.HasOne("WaveChat.Context.Entities.Messages.Message", "IdmessageNavigation")
                        .WithMany("Photos")
                        .HasForeignKey("Idmessage")
                        .HasConstraintName("photos_idmessage_fkey");

                    b.HasOne("WaveChat.Context.Entities.DashBoard.News", "IdnewNavigation")
                        .WithMany("Photos")
                        .HasForeignKey("Idnew")
                        .HasConstraintName("photos_idnew_fkey");

                    b.HasOne("WaveChat.Context.Entities.Users.User", "IduserNavigation")
                        .WithMany("Photos")
                        .HasForeignKey("Iduser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("photos_iduser_fkey");

                    b.Navigation("IdboardNavigation");

                    b.Navigation("IdchannelNavigation");

                    b.Navigation("IdmessageNavigation");

                    b.Navigation("IdnewNavigation");

                    b.Navigation("IduserNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Users.User", b =>
                {
                    b.HasOne("WaveChat.Context.Entities.Users.Rolestype", "RoletypeNavigation")
                        .WithMany("Users")
                        .HasForeignKey("Roletype")
                        .HasConstraintName("users_roletype_fkey");

                    b.Navigation("RoletypeNavigation");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Boards.Board", b =>
                {
                    b.Navigation("Dependencies");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.DashBoard.News", b =>
                {
                    b.Navigation("Newscomments");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Channel", b =>
                {
                    b.Navigation("Channelstypes");

                    b.Navigation("Messages");

                    b.Navigation("Photos");

                    b.Navigation("Userschannels");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Messages.Message", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Users.Rolestype", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WaveChat.Context.Entities.Users.User", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("Dependencies");

                    b.Navigation("Messages");

                    b.Navigation("Newscomments");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}