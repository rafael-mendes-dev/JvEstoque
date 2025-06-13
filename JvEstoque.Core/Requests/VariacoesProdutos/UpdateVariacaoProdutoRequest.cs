using System.ComponentModel.DataAnnotations;
using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Requests.VariacoesProdutos;

public class UpdateVariacaoProdutoRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O SKU é obrigatório.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O SKU deve ter entre 3 e 50 caracteres.")]
    public string Sku { get; set; } = string.Empty;
 
    public int ProdutoId { get; set; }
    public int EscolaId { get; set; }
     
    [Required(ErrorMessage = "O tamanho é obrigatório.")]
    public ETamanho Tamanho { get; set; } = ETamanho.Outro;
     
    [Required(ErrorMessage = "A cor é obrigatória.")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "A cor deve ter entre 1 e 30 caracteres.")]
    public string Cor { get; set; } = string.Empty;
     
    [StringLength(50, ErrorMessage = "O tecido não pode exceder 50 caracteres.")]
    public string? Tecido { get; set; }
     
    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public EGenero Genero { get; set; } = EGenero.Unissex;
     
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade no estoque deve ser maior que zero.")]
    public int Estoque { get; set; } = 0;
}