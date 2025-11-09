using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyConsole.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleRemovedFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
