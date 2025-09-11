using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyConsole.Data.Migrations
{
    /// <inheritdoc />
    public partial class PublicIdAddedToImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "CustomerImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "CustomerImages");
        }
    }
}
