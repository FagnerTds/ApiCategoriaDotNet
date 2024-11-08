using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mg)
        {
            mg.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "Values('Coca-Cola','Refrigerante de Cola 350ml', 5.45, 'coca.jpg',50,now(),1)");

            mg.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "Values('Hamburger','Hamburger de picanha', 35.45, 'hamburger.jpg',3,now(),2)");

            mg.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "Values('Pudim','Pudim da Dona Marcia', 10.0, 'pudim.jpg',22,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mg)
        {
            mg.Sql("Delete from Produtos");
        }
    }
}
