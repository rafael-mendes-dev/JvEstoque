namespace JvEstoque.Core.Requests.VariacoesProdutos;

public class GetAllVariacoesProdutosByProdutoIdRequest : PagedRequest
{
    public int ProdutoId { get; set; }
}