using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JvEstoque.Api.Migrations
{
    /// <inheritdoc />
    public partial class AumentadoTamanhoNomeProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "VariacaoProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produto",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(25)",
                oldMaxLength: 25);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "VariacaoProduto");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produto",
                type: "VARCHAR(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldMaxLength: 50);
        }
    }
}
