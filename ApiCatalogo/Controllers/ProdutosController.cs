using ApiCatalogo.Context;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public ProdutosController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet("produto/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>>GetProdutosCategoria(int id)
    {
        var produtos =_uof.ProdutoRepository.GetProdutoPorCategoria(id);
        if (produtos is null)
            return NotFound("Produto não encontrado");
        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null)
            return NotFound("Produtos não encontrados");
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado");
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var novoProduto = _uof.ProdutoRepository.Create(produtoDto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();

        var atualizado = _uof.ProdutoRepository.Update(produtoDto);
        _uof.Commit();


        return Ok(atualizado);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound($"Produto com id = {id} não foi encontrado");
        var deletado =_uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        return Ok(deletado);
    }

}
