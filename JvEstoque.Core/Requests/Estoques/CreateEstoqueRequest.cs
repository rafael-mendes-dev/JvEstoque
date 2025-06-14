using System.ComponentModel.DataAnnotations;
using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Requests.Estoques;

public class CreateEstoqueRequest
{
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int VariacaoProdutoId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}