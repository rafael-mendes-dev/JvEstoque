using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Requests.VariacoesProdutos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Pedidos;

public partial class CreatePedidoComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public CreatePedidoRequest InputModel { get; set; } = new();
    public IEnumerable<VariacaoProduto> VariacoesProdutos { get; set; } = Enumerable.Empty<VariacaoProduto>();
    
    // Campo para armazenar a variação selecionada no autocomplete
    public VariacaoProduto? VariacaoProdutoSelecionada;

    #endregion
    
    #region Parameters
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IPedidoHandler Handler { get; set; } = null!;
    [Inject] public IVariacaoProdutoHandler VariacaoProdutoHandler { get; set; } = null!;
    [CascadingParameter] public IMudDialogInstance DialogInstance { get; set; } = null!;
    
    #endregion
    
    #region Overrides
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            IsBusy = true;
            await LoadVariacoesProdutosAsync();
        }
        catch (System.Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    #endregion
    
    #region Methods
    
    private async Task LoadVariacoesProdutosAsync()
    {
        var response = await VariacaoProdutoHandler.GetAllAsync(new GetAllVariacoesProdutosRequest { PageSize = 9999 }); // Carrega todas as variações
        if (response.IsSucess)
            VariacoesProdutos = response.Data ?? Enumerable.Empty<VariacaoProduto>();
    }
    
    // Função de busca para o Autocomplete
    public Task<IEnumerable<VariacaoProduto>> SearchVariacoes(string value, CancellationToken token)
    {
        if (string.IsNullOrEmpty(value))
            return Task.FromResult(VariacoesProdutos);

        var result = VariacoesProdutos.Where(vp =>
            vp.Produto.Nome.Contains(value, StringComparison.OrdinalIgnoreCase) ||
            vp.Cor.Contains(value, StringComparison.OrdinalIgnoreCase) ||
            vp.Tamanho.Equals(StringComparison.OrdinalIgnoreCase));
            
        return Task.FromResult(result);
    }

    // Adiciona o item selecionado à lista do pedido
    public void AdicionarItem()
    {
        if (VariacaoProdutoSelecionada == null) return;

        if (InputModel.Itens.Any(i => i.VariacaoProdutoId == VariacaoProdutoSelecionada.Id))
        {
            Snackbar.Add("Este produto já foi adicionado.", Severity.Warning);
            return;
        }

        InputModel.Itens.Add(new CreateItemPedidoRequest
        {
            VariacaoProdutoId = VariacaoProdutoSelecionada.Id,
            Quantidade = 1
        });
        VariacaoProdutoSelecionada = null; // Limpa o autocomplete
    }

    // Remove um item da lista
    public void RemoverItem(CreateItemPedidoRequest item)
    {
        InputModel.Itens.Remove(item);
    }
    
    // Obtém a descrição da variação para exibir na tabela
    public string GetVariacaoProdutoDescricao(int variacaoId)
    {
        var variacao = VariacoesProdutos.FirstOrDefault(vp => vp.Id == variacaoId);
        return variacao != null ? $"{variacao.Produto.Nome} ({variacao.Cor} - {variacao.Tamanho})" : "Produto não encontrado";
    }
    
    public async Task OnValidSubmitAsync()
    {
        if (!InputModel.Itens.Any())
        {
            Snackbar.Add("O pedido deve conter pelo menos um item.", Severity.Error);
            return;
        }
        
        IsBusy = true;
        try
        {
            var response = await Handler.CreateAsync(InputModel);
            if (response is { IsSucess: false, Message: not null })
            {
                Snackbar.Add(response.Message, Severity.Error);
                return;
            }
            DialogInstance.Close(DialogResult.Ok(true));
        }
        catch (System.Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    #endregion
}