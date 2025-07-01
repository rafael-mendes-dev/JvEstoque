using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Escolas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Escolas;

public partial class CreateEscolaComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateEscolaRequest InputModel { get; set; } = new();

    #endregion

    #region Parameters
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IEscolaHandler Handler { get; set; } = null!;
    [CascadingParameter] IMudDialogInstance DialogInstance { get; set; } = null!;
    
    
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