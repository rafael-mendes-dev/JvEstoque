using System.ComponentModel.DataAnnotations;
using JvEstoque.Core.Enums;
using JvEstoque.Core.Requests.VariacoesProdutos;

namespace JvEstoque.Core.Requests.Produtos;

public class CreateProdutoRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 25 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O tipo de peça é obrigatória.")]
    public EPeca Peca { get; set; } = EPeca.Outro;
    
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }
    
    [StringLength(250, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string? Descricao { get; set; }
    
    [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")] 
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior que 0.")]
    public int QuantidadeEstoque { get; set; } = 1;
    
    
    public IList<CreateVariacaoProdutoRequest>? Variacoes { get; set; } = new List<CreateVariacaoProdutoRequest>();
}