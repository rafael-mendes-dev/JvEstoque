using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Models;

public class Pedido
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = null!;
    public string? TelefoneCliente { get; set; }

    public EStatusPedido Status { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }

    public IList<ItemPedido> Itens { get; set; } = null!;
}