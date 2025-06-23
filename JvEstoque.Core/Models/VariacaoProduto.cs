using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Models;

public class VariacaoProduto
{
    public int Id { get; set; }
    public string Sku { get; set; } = null!;
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    
    public int EscolaId { get; set; }
    public Escola Escola { get; set; } = null!;
    
    public ETamanho Tamanho { get; set; }
    public string Cor { get; set; } = null!;
    public string? Tecido { get; set; }
    public EGenero Genero { get; set; }

    public int EstoqueId { get; set; }
    public Estoque Estoque { get; set; } = null!;
    
    public IList<ItemPedido> ItensPedidos { get; set; } = new List<ItemPedido>();

}