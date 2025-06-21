using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JvEstoque.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSubTotalFromItemPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "ItemPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "ItemPedido",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
