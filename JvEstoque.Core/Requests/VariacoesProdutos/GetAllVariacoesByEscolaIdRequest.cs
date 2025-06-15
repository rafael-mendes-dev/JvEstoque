namespace JvEstoque.Core.Requests.VariacoesProdutos;

public class GetAllVariacoesByEscolaIdRequest : PagedRequest
{
    public int EscolaId { get; set; }
}