using System.ComponentModel.DataAnnotations;

namespace JvEstoque.Core.Requests.Account;

public class LoginRequest
{
    [Required(ErrorMessage = "Informe um e-mail.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Informe uma senha.")]
    public string Password { get; set; } = string.Empty;
}