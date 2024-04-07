using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    public partial class popularcategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) " +
                            "VALUES('Normal','Lanche feito com ingredientes normais')");

            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao) " +
                "VALUES('Natural','Lanche feito com ingredientes integrais e naturais')");

            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao) " +
                "VALUES('Sobremesas','Doces Deliciosos')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
