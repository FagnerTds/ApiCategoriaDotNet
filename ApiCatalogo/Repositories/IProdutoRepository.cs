using ApiCatalogo.Models;
using System.Runtime.InteropServices;

namespace ApiCatalogo.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutoPorCategoria(int id);
    }
}
