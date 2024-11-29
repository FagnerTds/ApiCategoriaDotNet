using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _maper;

    public ProdutosController(IUnitOfWork uof, IMapper maper)
    {
        _uof = uof;
        _maper = maper;
    }

    [HttpGet("produto/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>>GetProdutosCategoria(int id)
    {
        var produtos =_uof.ProdutoRepository.GetProdutoPorCategoria(id);
        if (produtos is null)
            return NotFound("Produto não encontrado");
        var produtoDto = _maper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null)
            return NotFound("Produtos não encontrados");
        var produtoDto = _maper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado");
        var produtoDto = _maper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();
        var produto = _maper.Map<Produto>(produtoDto);

        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDto = _maper.Map<ProdutoDTO>(novoProduto);
        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch (int id, 
        JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
    {
        if (patchProdutoDto is null || id <= 0)
            return BadRequest();
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound();
        var produtoUpdateRequest = _maper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        _maper.Map(produtoUpdateRequest, produto);

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        return Ok(_maper.Map<ProdutoDTOUpdateResponse>(produto));

    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();
        var produto = _maper.Map<Produto>(produtoDto);
        var atualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        var ProdutoAtualizadoDto = _maper.Map<ProdutoDTO>(atualizado);
        return Ok(ProdutoAtualizadoDto);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound($"Produto com id = {id} não foi encontrado");

        var deletado =_uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        var produtoDto = _maper.Map<ProdutoDTO>(deletado);
        return Ok(produtoDto);
    }

}
