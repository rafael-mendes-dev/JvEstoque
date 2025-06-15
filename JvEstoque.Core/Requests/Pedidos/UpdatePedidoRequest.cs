using System.ComponentModel.DataAnnotations;
using JvEstoque.Core.Enums;
using JvEstoque.Core.Requests.ItensPedidos;

namespace JvEstoque.Core.Requests.Pedidos;

public class UpdatePedidoRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome do cliente é obrigatório.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "O nome do cliente deve ter entre 3 e 120 caracteres.")]
    public string NomeCliente { get; set; } = string.Empty;
    
    [Phone(ErrorMessage = "Telefone inválido.")]
    public string? TelefoneCliente { get; set; }
    
    [Required(ErrorMessage = "Status inválido.")]
    public EStatusPedido Status { get; set; } = EStatusPedido.Recebido;
    
    public DateTime? DataPedido { get; set; }
    
    public IList<CreateItemPedidoRequest> Itens { get; set; } = new List<CreateItemPedidoRequest>();
}