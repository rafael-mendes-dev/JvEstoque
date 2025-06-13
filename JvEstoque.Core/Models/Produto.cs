using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Models;

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public EPeca Peca { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }

    public List<VariacaoProduto> Variacoes { get; set; }
}