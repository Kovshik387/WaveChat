using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaveChat.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "userschannels",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userschannels_UserId",
                table: "userschannels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userschannels_users_UserId",
                table: "userschannels",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userschannels_users_UserId",
                table: "userschannels");

            migrationBuilder.DropIndex(
                name: "IX_userschannels_UserId",
                table: "userschannels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userschannels");
        }
    }
}
