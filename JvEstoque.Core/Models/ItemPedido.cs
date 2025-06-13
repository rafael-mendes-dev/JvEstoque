namespace JvEstoque.Core.Models;

public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
    
    public int VariacaoId { get; set; }
    public VariacaoProduto Variacao { get; set; } = null!;
    
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal => Quantidade * ValorUnitario;
}