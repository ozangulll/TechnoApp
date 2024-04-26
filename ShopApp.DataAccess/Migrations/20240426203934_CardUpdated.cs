using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CardUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CartItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
