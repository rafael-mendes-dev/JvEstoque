using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JvEstoque.Core.Requests.VariacoesProdutos;

namespace JvEstoque.Core.Requests.Escolas;

public class CreateEscolaRequest
{
    [Required(ErrorMessage = "O nome da escola é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    
    [StringLength(120, ErrorMessage = "O endereço não pode exceder 120 caracteres.")]
    public string? Endereco { get; set; }
    
    [Phone(ErrorMessage = "Telefone inválido.")]
    [StringLength(15, ErrorMessage = "O telefone não pode exceder 15 caracteres.")]
    public string? Telefone { get; set; }
    
    [JsonIgnore]
    public IList<CreateVariacaoProdutoRequest>? UniformesEscola { get; set; } = new List<CreateVariacaoProdutoRequest>();
}