using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Models;

public class Pedido
{
    public int Id { get; set; }
    public string? NomeCliente { get; set; }
    public string? TelefoneCliente { get; set; }

    public EStatusPedido Status { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
    
    public List<ItemPedido> Itens { get; set; } = new();
}