using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WaveChat.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "channels",
                columns: table => new
                {
                    idchannel = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("channels_pkey", x => x.idchannel);
                });

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    idnew = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    likes = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("news_pkey", x => x.idnew);
                });

            migrationBuilder.CreateTable(
                name: "rolestypes",
                columns: table => new
                {
                    idroletype = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rolestypes_pkey", x => x.idroletype);
                });

            migrationBuilder.CreateTable(
                name: "statusboards",
                columns: table => new
                {
                    idstatusboard = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statusboards_pkey", x => x.idstatusboard);
                });

            migrationBuilder.CreateTable(
                name: "channelstypes",
                columns: table => new
                {
                    idchanneltype = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typename = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    idchannel = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("channelstypes_pkey", x => x.idchanneltype);
                    table.ForeignKey(
                        name: "channelstypes_idchannel_fkey",
                        column: x => x.idchannel,
                        principalTable: "channels",
                        principalColumn: "idchannel");
                });

            migrationBuilder.CreateTable(
                name: "userschannels",
                columns: table => new
                {
                    iduserchannel = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    channelid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userschannels_pkey", x => x.iduserchannel);
                    table.ForeignKey(
                        name: "userschannels_channelid_fkey",
                        column: x => x.channelid,
                        principalTable: "channels",
                        principalColumn: "idchannel");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    passwordhash = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    registrationdate = table.Column<DateOnly>(type: "date", nullable: false),
                    lastvisitdate = table.Column<DateOnly>(type: "date", nullable: false),
                    roletype = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.iduser);
                    table.ForeignKey(
                        name: "users_roletype_fkey",
                        column: x => x.roletype,
                        principalTable: "rolestypes",
                        principalColumn: "idroletype");
                });

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    idboard = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    issuedate = table.Column<DateOnly>(type: "date", nullable: false),
                    deadlinedate = table.Column<DateOnly>(type: "date", nullable: false),
                    userboard = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("boards_pkey", x => x.idboard);
                    table.ForeignKey(
                        name: "boards_userboard_fkey",
                        column: x => x.userboard,
                        principalTable: "users",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    idmessage = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    senddate = table.Column<DateOnly>(type: "date", nullable: false),
                    isread = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    idchannel = table.Column<int>(type: "integer", nullable: false),
                    iduser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("message_pkey", x => x.idmessage);
                    table.ForeignKey(
                        name: "message_idchannel_fkey",
                        column: x => x.idchannel,
                        principalTable: "channels",
                        principalColumn: "idchannel");
                    table.ForeignKey(
                        name: "message_iduser_fkey",
                        column: x => x.iduser,
                        principalTable: "users",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateTable(
                name: "newscomments",
                columns: table => new
                {
                    idcomment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: false),
                    commentdate = table.Column<DateOnly>(type: "date", nullable: false),
                    idnew = table.Column<int>(type: "integer", nullable: false),
                    iduser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("newscomments_pkey", x => x.idcomment);
                    table.ForeignKey(
                        name: "newscomments_idnew_fkey",
                        column: x => x.idnew,
                        principalTable: "news",
                        principalColumn: "idnew");
                    table.ForeignKey(
                        name: "newscomments_iduser_fkey",
                        column: x => x.iduser,
                        principalTable: "users",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateTable(
                name: "dependencies",
                columns: table => new
                {
                    iddependency = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idboard = table.Column<int>(type: "integer", nullable: false),
                    iduser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("dependencies_pkey", x => x.iddependency);
                    table.ForeignKey(
                        name: "dependencies_idboard_fkey",
                        column: x => x.idboard,
                        principalTable: "boards",
                        principalColumn: "idboard");
                    table.ForeignKey(
                        name: "dependencies_iduser_fkey",
                        column: x => x.iduser,
                        principalTable: "users",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    idphoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bucket = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    iduser = table.Column<int>(type: "integer", nullable: true),
                    idboard = table.Column<int>(type: "integer", nullable: true),
                    idnew = table.Column<int>(type: "integer", nullable: true),
                    idmessage = table.Column<int>(type: "integer", nullable: true),
                    idchannel = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("photos_pkey", x => x.idphoto);
                    table.ForeignKey(
                        name: "photos_idboard_fkey",
                        column: x => x.idboard,
                        principalTable: "boards",
                        principalColumn: "idboard");
                    table.ForeignKey(
                        name: "photos_idchannel_fkey",
                        column: x => x.idchannel,
                        principalTable: "channels",
                        principalColumn: "idchannel");
                    table.ForeignKey(
                        name: "photos_idmessage_fkey",
                        column: x => x.idmessage,
                        principalTable: "message",
                        principalColumn: "idmessage");
                    table.ForeignKey(
                        name: "photos_idnew_fkey",
                        column: x => x.idnew,
                        principalTable: "news",
                        principalColumn: "idnew");
                    table.ForeignKey(
                        name: "photos_iduser_fkey",
                        column: x => x.iduser,
                        principalTable: "users",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_boards_userboard",
                table: "boards",
                column: "userboard");

            migrationBuilder.CreateIndex(
                name: "IX_channelstypes_idchannel",
                table: "channelstypes",
                column: "idchannel");

            migrationBuilder.CreateIndex(
                name: "IX_dependencies_idboard",
                table: "dependencies",
                column: "idboard");

            migrationBuilder.CreateIndex(
                name: "IX_dependencies_iduser",
                table: "dependencies",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_message_idchannel",
                table: "message",
                column: "idchannel");

            migrationBuilder.CreateIndex(
                name: "IX_message_iduser",
                table: "message",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_newscomments_idnew",
                table: "newscomments",
                column: "idnew");

            migrationBuilder.CreateIndex(
                name: "IX_newscomments_iduser",
                table: "newscomments",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_photos_idboard",
                table: "photos",
                column: "idboard");

            migrationBuilder.CreateIndex(
                name: "IX_photos_idchannel",
                table: "photos",
                column: "idchannel");

            migrationBuilder.CreateIndex(
                name: "IX_photos_idmessage",
                table: "photos",
                column: "idmessage");

            migrationBuilder.CreateIndex(
                name: "IX_photos_idnew",
                table: "photos",
                column: "idnew");

            migrationBuilder.CreateIndex(
                name: "IX_photos_iduser",
                table: "photos",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_users_roletype",
                table: "users",
                column: "roletype");

            migrationBuilder.CreateIndex(
                name: "IX_userschannels_channelid",
                table: "userschannels",
                column: "channelid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channelstypes");

            migrationBuilder.DropTable(
                name: "dependencies");

            migrationBuilder.DropTable(
                name: "newscomments");

            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "statusboards");

            migrationBuilder.DropTable(
                name: "userschannels");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rolestypes");
        }
    }
}
