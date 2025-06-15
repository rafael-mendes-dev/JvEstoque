namespace JvEstoque.Core.Models;

public class Estoque
{
    public int Id { get; set; }
    public int VariacaoProdutoId { get; set; }
    public VariacaoProduto Variacao { get; set; } = null!;
    public int Quantidade { get; set; }
}