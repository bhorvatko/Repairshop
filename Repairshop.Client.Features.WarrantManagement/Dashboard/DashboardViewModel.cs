﻿using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Configuration;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class DashboardViewModel
    : ViewModelBase
{
    private readonly TechnicianDashboardViewModelFactory _technicianDashboardViewModelFactory;
    private readonly IUserSettingsProvider<WarrantManagementConfiguration> _userSettingsProvider;

    private IReadOnlyCollection<TechnicianDashboardViewModel> _technicianDashboards = 
        new List<TechnicianDashboardViewModel>();

    public DashboardViewModel(
        TechnicianDashboardViewModelFactory technicianDashboardViewModelFactory,
        IUserSettingsProvider<WarrantManagementConfiguration> userSettingsProvider)
    {
        _technicianDashboardViewModelFactory = technicianDashboardViewModelFactory;
        _userSettingsProvider = userSettingsProvider;
    }

    public IReadOnlyCollection<TechnicianDashboardViewModel> TechnicianDashboards
    {
        get => _technicianDashboards;
        set => SetTechnicianDashboards(value);
    }

    [RelayCommand]
    public async Task LoadTechnicians()
    {
        IEnumerable<Guid?> technicianIds = 
            _userSettingsProvider.GetSettings().TechnicianDashboards.Select(x => x.TechnicianId);

        Guid?[] viewModelIds = Enumerable.Repeat<Guid?>(null, 3).ToArray();

        for (int i = 0; i < viewModelIds.Count(); i++)
        {
            viewModelIds[i] = technicianIds.ElementAtOrDefault(i);
        }

        TechnicianDashboards =
            await _technicianDashboardViewModelFactory.CreateViewModel(viewModelIds);
    }

    public override void OnNavigatedAway()
    {
        WarrantManagementConfiguration userSettings = _userSettingsProvider.GetSettings() with
        {
            TechnicianDashboards = TechnicianDashboards
                .Select(x => new TechnicianDashboardConfiguration() 
                { 
                    TechnicianId = x.SelectedTechnician?.Id 
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
}
