﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class ProcedureLegendViewModel
    : ObservableObject
{
    private readonly IProcedureService _procedureService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    [ObservableProperty]
    private IReadOnlyCollection<ProcedureSummaryViewModel> _procedures =
        new List<ProcedureSummaryViewModel>();

    public ProcedureLegendViewModel(
        IProcedureService procedureService, 
        ILoadingIndicatorService loadingIndicatorService)
    {
        _procedureService = procedureService;
        _loadingIndicatorService = loadingIndicatorService;
    }

    [RelayCommand]
    public async Task OnLoaded()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            Procedures = (await _procedureService.GetProcedureSummaries()).ToList();
        });
    }
}
