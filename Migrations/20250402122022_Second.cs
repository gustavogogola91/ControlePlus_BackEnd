using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControlePlus_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_produto_Fornecedor_FornecedorId",
                table: "tb_produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fornecedor",
                table: "Fornecedor");

            migrationBuilder.RenameTable(
                name: "Fornecedor",
                newName: "tb_fornecedor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_fornecedor",
                table: "tb_fornecedor",
                column: "id");

            migrationBuilder.CreateTable(
                name: "tb_historico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_historico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_historico_tb_produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tb_produto",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_historico_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_historico_ProdutoId",
                table: "tb_historico",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_historico_UsuarioId",
                table: "tb_historico",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_produto_tb_fornecedor_FornecedorId",
                table: "tb_produto",
                column: "FornecedorId",
                principalTable: "tb_fornecedor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_produto_tb_fornecedor_FornecedorId",
                table: "tb_produto");

            migrationBuilder.DropTable(
                name: "tb_historico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_fornecedor",
                table: "tb_fornecedor");

            migrationBuilder.RenameTable(
                name: "tb_fornecedor",
                newName: "Fornecedor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fornecedor",
                table: "Fornecedor",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_produto_Fornecedor_FornecedorId",
                table: "tb_produto",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
