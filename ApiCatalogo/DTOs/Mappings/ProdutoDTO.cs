using ApiCatalogo.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiCatalogo.DTOs.Mappings
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = " O nome é obrigatório")]
        [StringLength(80, ErrorMessage = " O nome deve ter entre 5 e 20 caracteres",
            MinimumLength = 5)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = " a descrição deve ter no máximo {1} Caracteres")]
        public string? Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        
        public int CategoriaId { get; set; }

        

    }
}
