﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PolpulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias (Nome, ImagemUrl) Values ('Bebidas', 'bebidas.jpg')");
            mb.Sql("Insert into Categorias (Nome, ImagemUrl) Values ('Lanches', 'lanches.jpg')");
            mb.Sql("Insert into Categorias (Nome, ImagemUrl) Values ('Sobremesas', 'sobremesas.jpg')");
            mb.Sql("Insert into Categorias (Nome, ImagemUrl) Values ('Suplementos', 'suplemento.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");

        }
    }
}
