using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Models;

public class Pedido
{
    public long Id { get; set; }
    
    public EStatusPedido Status { get; set; }
    
    public DateTime DataPedido { get; set; } = DateTime.Now;
    
    public long ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public decimal Valor { get; set; }
}