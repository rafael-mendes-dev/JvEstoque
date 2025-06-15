namespace JvEstoque.Core.Requests.VariacoesProdutos;

public class GetAllVariacoesProdutosByEscolaIdRequest : PagedRequest
{
    public int EscolaId { get; set; }
}