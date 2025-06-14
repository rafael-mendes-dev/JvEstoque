using System.ComponentModel.DataAnnotations;

namespace JvEstoque.Core.Requests.Estoques;

public class UpdateEstoqueRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int VariacaoProdutoId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    public int Quantidade { get; set; }
}