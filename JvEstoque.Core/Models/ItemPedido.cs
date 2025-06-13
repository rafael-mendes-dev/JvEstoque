namespace JvEstoque.Core.Models;

public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
    
    public int VariacaoProdutoId { get; set; }
    public VariacaoProduto VariacaoProduto { get; set; } = null!;
    
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    
    public decimal SubTotal { get; set; }
}