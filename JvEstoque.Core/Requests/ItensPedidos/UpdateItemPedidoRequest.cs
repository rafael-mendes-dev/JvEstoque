using System.ComponentModel.DataAnnotations;

namespace JvEstoque.Core.Requests.ItensPedidos;

public class UpdateItemPedidoRequest
{
    public int Id { get; set; }
    
    public int VariacaoProdutoId { get; set; }
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}