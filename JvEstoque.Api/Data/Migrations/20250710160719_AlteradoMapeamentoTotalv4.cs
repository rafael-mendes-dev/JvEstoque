using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JvEstoque.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoMapeamentoTotalv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_VariacaoProduto_VariacaoProdutoId",
                table: "ItemPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_VariacaoProduto_VariacaoProdutoId",
                table: "ItemPedido",
                column: "VariacaoProdutoId",
                principalTable: "VariacaoProduto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedido_VariacaoProduto_VariacaoProdutoId",
                table: "ItemPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedido_VariacaoProduto_VariacaoProdutoId",
                table: "ItemPedido",
                column: "VariacaoProdutoId",
                principalTable: "VariacaoProduto",
                principalColumn: "Id");
        }
    }
}
