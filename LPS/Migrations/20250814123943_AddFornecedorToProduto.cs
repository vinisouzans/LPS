using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPS.Migrations
{
    /// <inheritdoc />
    public partial class AddFornecedorToProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Fornecedor_FornecedorId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedor_FornecedorId",
                table: "Produtos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantidade",
                table: "Vendas",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeTotal",
                table: "Estoques",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeDisponivel",
                table: "Estoques",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "Estoques",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_EstoqueId",
                table: "Estoques",
                column: "EstoqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Estoques_EstoqueId",
                table: "Estoques",
                column: "EstoqueId",
                principalTable: "Estoques",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Fornecedor_FornecedorId",
                table: "Estoques",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Fornecedor_FornecedorId",
                table: "Produtos",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Estoques_EstoqueId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Fornecedor_FornecedorId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedor_FornecedorId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_EstoqueId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "Estoques");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantidade",
                table: "Vendas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeTotal",
                table: "Estoques",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantidadeDisponivel",
                table: "Estoques",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Fornecedor_FornecedorId",
                table: "Estoques",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Fornecedor_FornecedorId",
                table: "Produtos",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
