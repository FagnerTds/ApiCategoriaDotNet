﻿using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public CategoriasController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _uof.CategoriaRepository.GetAll();
            if (categorias is null) 
                return NotFound("Não existem categorias");
            var categoriasDto = categorias.ToCategoriaDtoList();

            return Ok(categoriasDto);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public  ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null)
                return NotFound($"Categoria com id={id} não pode ser encontrada...");
            var categoriaDto = categoria.ToCategoriaDto();

            return Ok(categoriaDto);
        }


        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return BadRequest("Dados inválidos");

            var categoria = categoriaDto.ToCategoria();
            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDto();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);

        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
                return BadRequest();
            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            var novaCategoriaDto = categoriaAtualizada.ToCategoriaDto();
            return Ok(novaCategoriaDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {

            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null)
                return NotFound($"Categoria com id={id} não pode ser encontrada...");

            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaExcluidaDto = categoria.ToCategoriaDto();

            return Ok(categoriaExcluidaDto);
        }

    }
}
