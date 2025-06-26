using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JvEstoque.Core.Requests.Estoques;

public class UpdateEstoqueRequest
{
    public int Id { get; set; }
    [JsonIgnore]
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int VariacaoProdutoId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    public int Quantidade { get; set; }
}