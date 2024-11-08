using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ProdutosController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _appDbContext.Produtos.ToList();
        if(produtos is null)
            return NotFound("Produtos não encontrados");
        return produtos;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado");
        return produto;
    }
}
