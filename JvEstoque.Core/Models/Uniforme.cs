namespace JvEstoque.Core.Models;

public class Uniforme
{
    public int Id { get; set; }
    
    public string Peca { get; set; } = string.Empty;

    public string Escola { get; set; } = string.Empty;
    public string Tamanho { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
}