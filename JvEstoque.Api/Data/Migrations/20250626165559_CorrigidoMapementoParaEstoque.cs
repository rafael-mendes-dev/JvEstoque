using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JvEstoque.Api.Migrations
{
    /// <inheritdoc />
    public partial class CorrigidoMapementoParaEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "VariacaoProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "VariacaoProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
