using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Reports;

public partial class ItensEmBaixoEstoqueComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public int BaixoEstoque { get; set; }

    #endregion

    #region Services

    [Inject] private IReportHandler Handler { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var response = await Handler.GetItensEmBaixoEstoqueAsync(new GetItensEmBaixoEstoqueRequest());
            if (response.IsSucess && response.Data != null)
            {
                BaixoEstoque = response.Data.Quantidade;
            }
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