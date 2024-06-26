using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace waves_server.Migrations
{
    /// <inheritdoc />
    public partial class IndexesCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId_Username_Email",
                table: "Users",
                columns: new[] { "UserId", "Username", "Email" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserId_Username_Email",
                table: "Users");
        }
    }
}
