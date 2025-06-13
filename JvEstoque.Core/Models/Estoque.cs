namespace JvEstoque.Core.Models;

public class Estoque
{
    public int VariacaoId { get; set; }
    public VariacaoProduto Variacao { get; set; } = null!;
    public int Quantidade { get; set; }
}