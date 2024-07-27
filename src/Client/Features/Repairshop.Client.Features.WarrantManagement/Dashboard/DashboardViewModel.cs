﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.ClientContext;
using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Configuration;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Common.ClientContext;
using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class DashboardViewModel
    : ViewModelBase
{
    private readonly TechnicianDashboardViewModelFactory _technicianDashboardViewModelFactory;
    private readonly IUserSettingsProvider<WarrantManagementConfiguration> _userSettingsProvider;
    private readonly IFormService _formService;
    private readonly IClientContextProvider _clientContextProvider;
    [ObservableProperty]
    private ProcedureLegendViewModel _procedureLegendViewModel;

    private IReadOnlyCollection<TechnicianDashboardViewModel> _technicianDashboards = 
        new List<TechnicianDashboardViewModel>();

    public DashboardViewModel(
        TechnicianDashboardViewModelFactory technicianDashboardViewModelFactory,
        IUserSettingsProvider<WarrantManagementConfiguration> userSettingsProvider,
        ProcedureLegendViewModel procedureLegendViewModel,
        IFormService formService,
        IClientContextProvider clientContextProvider)
    {
        _technicianDashboardViewModelFactory = technicianDashboardViewModelFactory;
        _userSettingsProvider = userSettingsProvider;
        _formService = formService;
        _clientContextProvider = clientContextProvider;

        ProcedureLegendViewModel = procedureLegendViewModel;
    }

    public IReadOnlyCollection<TechnicianDashboardViewModel> TechnicianDashboards
    {
        get => _technicianDashboards;
        set => SetTechnicianDashboards(value);
    }

    public Visibility AddWarrantButtonVisibility => 
        (_clientContextProvider.GetClientContext() == RepairshopClientContext.FrontOffice).ToVisibility();   

    [RelayCommand]
    public async Task LoadTechnicians()
    {
        IEnumerable<TechnicianDashboardConfiguration> configurations =
            _userSettingsProvider.GetSettings().TechnicianDashboards;

        TechnicianDashboardConfiguration[] viewModelConfigurations =
            new TechnicianDashboardConfiguration[3];

        for (int i = 0; i < viewModelConfigurations.Count(); i++)
        {
            viewModelConfigurations[i] =
                configurations.ElementAtOrDefault(i) 
                    ?? TechnicianDashboardConfiguration.CreateDefault();
        }

        TechnicianDashboards =
            await _technicianDashboardViewModelFactory.CreateViewModels(viewModelConfigurations);
    }

    public override void OnNavigatedAway()
    {
        WarrantManagementConfiguration userSettings = _userSettingsProvider.GetSettings() with
        {
            TechnicianDashboards = TechnicianDashboards
                .Select(x => new TechnicianDashboardConfiguration()
                {
                    TechnicianId = x.SelectedTechnician?.Id,
                    ProcedureFilters = x.GetFilteredProcedureIds()
                })
        };

        _userSettingsProvider.SaveSettings(userSettings);
    }

    public override void Dispose()
    {
        DisposeTechnicianDashboard();
        base.Dispose();
    }

    private void SetTechnicianDashboards(IReadOnlyCollection<TechnicianDashboardViewModel> newTechnicianDashboards)
    {
        DisposeTechnicianDashboard();
        SetProperty(ref _technicianDashboards, newTechnicianDashboards, nameof(TechnicianDashboards));
    }

    private void DisposeTechnicianDashboard()
    {
        foreach (TechnicianDashboardViewModel technicianDashboardViewModel in _technicianDashboards)
        {
            technicianDashboardViewModel.Dispose();
        }
    }

    [RelayCommand]
    private async Task OnAddWarrant()
    {
        await _formService.ShowFormAsDialog<CreateWarrantView>();
    }
}