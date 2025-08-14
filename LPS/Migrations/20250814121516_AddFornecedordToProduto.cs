using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPS.Migrations
{
    /// <inheritdoc />
    public partial class AddFornecedordToProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Fornecedor_FornecedorId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Estoque_LoteId",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produtos_ProdutoId",
                table: "Venda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Venda",
                table: "Venda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estoque",
                table: "Estoque");

            migrationBuilder.RenameTable(
                name: "Venda",
                newName: "Vendas");

            migrationBuilder.RenameTable(
                name: "Estoque",
                newName: "Estoques");

            migrationBuilder.RenameIndex(
                name: "IX_Venda_ProdutoId",
                table: "Vendas",
                newName: "IX_Vendas_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Venda_LoteId",
                table: "Vendas",
                newName: "IX_Vendas_LoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoques",
                newName: "IX_Estoques_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoque_FornecedorId",
                table: "Estoques",
                newName: "IX_Estoques_FornecedorId");

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendas",
                table: "Vendas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estoques",
                table: "Estoques",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Estoques_LoteId",
                table: "Vendas",
                column: "LoteId",
                principalTable: "Estoques",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Produtos_ProdutoId",
                table: "Vendas",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Estoques_LoteId",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Produtos_ProdutoId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendas",
                table: "Vendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estoques",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Vendas",
                newName: "Venda");

            migrationBuilder.RenameTable(
                name: "Estoques",
                newName: "Estoque");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_ProdutoId",
                table: "Venda",
                newName: "IX_Venda_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_LoteId",
                table: "Venda",
                newName: "IX_Venda_LoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoque",
                newName: "IX_Estoque_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoques_FornecedorId",
                table: "Estoque",
                newName: "IX_Estoque_FornecedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venda",
                table: "Venda",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estoque",
                table: "Estoque",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Fornecedor_FornecedorId",
                table: "Estoque",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produtos_ProdutoId",
                table: "Estoque",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Estoque_LoteId",
                table: "Venda",
                column: "LoteId",
                principalTable: "Estoque",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produtos_ProdutoId",
                table: "Venda",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
