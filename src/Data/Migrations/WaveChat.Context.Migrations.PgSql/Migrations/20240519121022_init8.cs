using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveChat.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_userid_fkey",
                table: "userschannels");

            migrationBuilder.AddForeignKey(
                name: "userschannels_userid_fkey",
                table: "userschannels",
                column: "userid",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "userschannels_userid_fkey",
                table: "userschannels");

            migrationBuilder.AddForeignKey(
                name: "user_userid_fkey",
                table: "userschannels",
                column: "userid",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
