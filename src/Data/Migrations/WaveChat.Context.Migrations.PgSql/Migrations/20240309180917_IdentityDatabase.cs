using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WaveChat.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class IdentityDatabase : Migration
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
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                    likes = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                    rolename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statusboards_pkey", x => x.idstatusboard);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "channelstypes",
                columns: table => new
                {
                    idchanneltype = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typename = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    idchannel = table.Column<int>(type: "integer", nullable: true),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                    channelid = table.Column<int>(type: "integer", nullable: true),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    registrationdate = table.Column<DateOnly>(type: "date", nullable: false),
                    lastvisitdate = table.Column<DateOnly>(type: "date", nullable: false),
                    roletype = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "users_roletype_fkey",
                        column: x => x.roletype,
                        principalTable: "rolestypes",
                        principalColumn: "idroletype");
                });

            migrationBuilder.CreateTable(
                name: "user_role_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_role_claims_user_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "user_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    userboard = table.Column<Guid>(type: "uuid", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("boards_pkey", x => x.idboard);
                    table.ForeignKey(
                        name: "boards_userboard_fkey",
                        column: x => x.userboard,
                        principalTable: "users",
                        principalColumn: "Id");
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
                    iduser = table.Column<Guid>(type: "uuid", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                        principalColumn: "Id");
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
                    iduser = table.Column<Guid>(type: "uuid", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_user_logins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role_owners",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_owners", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_role_owners_user_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "user_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_owners_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dependencies",
                columns: table => new
                {
                    iddependency = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idboard = table.Column<int>(type: "integer", nullable: false),
                    iduser = table.Column<Guid>(type: "uuid", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    idphoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bucket = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    iduser = table.Column<Guid>(type: "uuid", nullable: true),
                    idboard = table.Column<int>(type: "integer", nullable: true),
                    idnew = table.Column<int>(type: "integer", nullable: true),
                    idmessage = table.Column<int>(type: "integer", nullable: true),
                    idchannel = table.Column<int>(type: "integer", nullable: true),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_boards_Uid",
                table: "boards",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_boards_userboard",
                table: "boards",
                column: "userboard");

            migrationBuilder.CreateIndex(
                name: "IX_channels_Uid",
                table: "channels",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_channelstypes_idchannel",
                table: "channelstypes",
                column: "idchannel");

            migrationBuilder.CreateIndex(
                name: "IX_channelstypes_Uid",
                table: "channelstypes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dependencies_idboard",
                table: "dependencies",
                column: "idboard");

            migrationBuilder.CreateIndex(
                name: "IX_dependencies_iduser",
                table: "dependencies",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_dependencies_Uid",
                table: "dependencies",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_message_idchannel",
                table: "message",
                column: "idchannel");

            migrationBuilder.CreateIndex(
                name: "IX_message_iduser",
                table: "message",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_message_Uid",
                table: "message",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_news_Uid",
                table: "news",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_newscomments_idnew",
                table: "newscomments",
                column: "idnew");

            migrationBuilder.CreateIndex(
                name: "IX_newscomments_iduser",
                table: "newscomments",
                column: "iduser");

            migrationBuilder.CreateIndex(
                name: "IX_newscomments_Uid",
                table: "newscomments",
                column: "Uid",
                unique: true);

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
                name: "IX_photos_Uid",
                table: "photos",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rolestypes_Uid",
                table: "rolestypes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_statusboards_Uid",
                table: "statusboards",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_UserId",
                table: "user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_UserId",
                table: "user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_claims_RoleId",
                table: "user_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_owners_RoleId",
                table: "user_role_owners",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "user_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_users_roletype",
                table: "users",
                column: "roletype");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userschannels_channelid",
                table: "userschannels",
                column: "channelid");

            migrationBuilder.CreateIndex(
                name: "IX_userschannels_Uid",
                table: "userschannels",
                column: "Uid",
                unique: true);
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
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_role_claims");

            migrationBuilder.DropTable(
                name: "user_role_owners");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "userschannels");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rolestypes");
        }
    }
}
