using ApiCatalogo.Context;
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
        private readonly ICategoriaRepository _repository;
        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("produtos")]
        //public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProduto()
        //{
        //    return await _repository.Categorias.Include(p => p.Produtos).ToListAsync();
        //}


        [HttpGet]
        public  ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public  ActionResult<Categoria> Get(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria is null)
                return NotFound($"Categoria com id={id} não pode ser encontrada...");
            return Ok(categoria);
        }


        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest("Dados inválidos");

            var categoriaCriada = _repository.Create(categoria);
            

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _repository.Update(categoria);            

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var categoria = _repository.GetCategoria(id);
            if (categoria is null)
                return NotFound($"Categoria com id={id} não pode ser encontrada...");

            var categoriaExcluida = _repository.Delete(id);

            return Ok(categoriaExcluida);
        }

    }
}
