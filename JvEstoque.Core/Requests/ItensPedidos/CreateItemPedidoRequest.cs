using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JvEstoque.Core.Requests.ItensPedidos;

public class CreateItemPedidoRequest
{
    [JsonIgnore]
    public int PedidoId { get; set; }
    public int VariacaoProdutoId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}