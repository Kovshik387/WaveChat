using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveChat.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userschannels_users_UserId",
                table: "userschannels");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "userschannels",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_userschannels_UserId",
                table: "userschannels",
                newName: "IX_userschannels_userid");

            migrationBuilder.AddForeignKey(
                name: "user_userid_fkey",
                table: "userschannels",
                column: "userid",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_userid_fkey",
                table: "userschannels");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "userschannels",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userschannels_userid",
                table: "userschannels",
                newName: "IX_userschannels_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userschannels_users_UserId",
                table: "userschannels",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
