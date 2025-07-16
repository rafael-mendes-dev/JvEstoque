using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Reports;

public partial class FaturamentoComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public decimal Faturamento { get; set; }

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
            var response = await Handler.GetFaturamentoAsync(new GetFaturamentoRequest());
            if (response.IsSucess && response.Data != null)
            {
                Faturamento = response.Data.Entradas;
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