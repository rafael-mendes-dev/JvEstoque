namespace JvEstoque.Core.Requests;

public class PagedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}