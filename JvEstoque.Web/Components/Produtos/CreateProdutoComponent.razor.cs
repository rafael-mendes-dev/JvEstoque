using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Produtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Produtos;

public partial class CreateProdutoComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateProdutoRequest InputModel { get; set; } = new();

    #endregion

    #region Parameters
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IProdutoHandler Handler { get; set; } = null!;
    [CascadingParameter] public IMudDialogInstance DialogInstance { get; set; } = null!;
    
    
    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
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
            StateHasChanged();
        }
        catch (Exception e)
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