namespace JvEstoque.Core.Models;

public class Estoque
{
    public int VariacaoProdutoId { get; set; }
    public VariacaoProduto Variacao { get; set; } = null!;
    public int Quantidade { get; set; }
}