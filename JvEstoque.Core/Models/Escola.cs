namespace JvEstoque.Core.Models;

public class Escola
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public string? Telefone { get; set; }

    public IList<VariacaoProduto>? VariacoesProdutos { get; set; }
}