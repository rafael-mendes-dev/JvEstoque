using System.ComponentModel.DataAnnotations;

namespace JvEstoque.Core.Requests.ItensPedidos;

public class CreateItemPedidoRequest
{
    public int PedidoId { get; set; }
    public int VariacaoProdutoId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
    
    [Required(ErrorMessage = "O valor unitário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior que zero.")]
    public decimal ValorUnitario { get; set; }
    
}