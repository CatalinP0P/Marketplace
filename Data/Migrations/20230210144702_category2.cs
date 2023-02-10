using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Data.Migrations
{
    /// <inheritdoc />
    public partial class category2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Anunturi",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Anunturi");
        }
    }
}
