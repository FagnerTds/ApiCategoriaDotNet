﻿using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {  
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutoPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}
