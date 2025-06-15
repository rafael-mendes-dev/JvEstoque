using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JvEstoque.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: true),
                    Telefone = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    TelefoneCliente = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: true),
                    Status = table.Column<short>(type: "SMALLINT", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "MONEY", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(25)", maxLength: 25, nullable: false),
                    Peca = table.Column<short>(type: "SMALLINT", nullable: false),
                    Preco = table.Column<decimal>(type: "MONEY", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VariacaoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    Tamanho = table.Column<short>(type: "SMALLINT", nullable: false),
                    Cor = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    Tecido = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Genero = table.Column<short>(type: "SMALLINT", nullable: false),
                    FotoUrl = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: true),
                    EstoqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariacaoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariacaoProduto_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariacaoProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariacaoProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoque_VariacaoProduto_VariacaoProdutoId",
                        column: x => x.VariacaoProdutoId,
                        principalTable: "VariacaoProduto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    VariacaoProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "INT", nullable: false, defaultValue: 1),
                    ValorUnitario = table.Column<decimal>(type: "MONEY", nullable: false),
                    SubTotal = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedido_VariacaoProduto_VariacaoProdutoId",
                        column: x => x.VariacaoProdutoId,
                        principalTable: "VariacaoProduto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_VariacaoProdutoId",
                table: "Estoque",
                column: "VariacaoProdutoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_VariacaoProdutoId",
                table: "ItemPedido",
                column: "VariacaoProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_VariacaoProduto_EscolaId",
                table: "VariacaoProduto",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_VariacaoProduto_ProdutoId",
                table: "VariacaoProduto",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "VariacaoProduto");

            migrationBuilder.DropTable(
                name: "Escola");

            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
