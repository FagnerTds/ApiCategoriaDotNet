using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var produtos = _appDbContext.Produtos.AsNoTracking().ToList();
        if(produtos is null)
            return NotFound("Produtos não encontrados");
        return produtos;
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado");
        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        _appDbContext.Produtos.Add(produto);
        _appDbContext.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Produto produto)
    {
        if(id != produto.ProdutoId)
            return BadRequest();

        _appDbContext.Entry(produto).State = EntityState.Modified;
        _appDbContext.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto is null) 
            return NotFound("Produto não localizado");

        _appDbContext.Remove(produto);
        _appDbContext.SaveChanges();

        return Ok(produto);
    }
   
}
