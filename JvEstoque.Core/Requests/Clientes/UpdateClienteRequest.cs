using System.ComponentModel.DataAnnotations;

namespace JvEstoque.Core.Requests.Clientes;

public class UpdateClienteRequest
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Insira um nome para o cliente.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do cliente deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [StringLength(21, MinimumLength = 9, ErrorMessage = "O telefone do cliente deve ter entre 9 e 21 caracteres.")]
    public string Telefone { get; set; } = string.Empty;
}