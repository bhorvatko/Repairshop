using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class WarrantPreviewControlViewModel
    : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IWarrantService _warrantService;
    private string _labelContent;

    public WarrantPreviewControlViewModel(
        WarrantSummaryViewModel warrant,
        INavigationService navigationService,
        IWarrantService warrantService)
    {
        Warrant = warrant;
        _navigationService = navigationService;
        _warrantService = warrantService;
        _labelContent = warrant.Title;
    }

    public WarrantSummaryViewModel Warrant { get; set; }
    public string LabelContent { get => _labelContent; set => SetProperty(ref _labelContent, value); }
    public bool PlayUpdateAnimation { get; set; }

    [RelayCommand]
    public async Task UpdateWarrant()
    {
        WarrantViewModel warrant =
            await _warrantService.GetWarrant(Warrant.Id);

        _navigationService.NavigateToView<UpdateWarrantView, UpdateWarrantViewModel>(vm =>
        {
            vm.WarrantId = warrant.Id;
            vm.EditWarrantViewModel.Subject = warrant.Title;
            vm.EditWarrantViewModel.Deadline = warrant.Deadline;
            vm.EditWarrantViewModel.IsUrgent = warrant.IsUrgent;
            vm.EditWarrantViewModel.Steps = warrant.Steps;
        });
    }

    [RelayCommand]
    public async Task AdvanceWarrant()
    {
        if (Warrant.NextStepId is null)
        {
            return;
        }

        await _warrantService.AdvanceWarrant(
            Warrant.Id, 
            Warrant.NextStepId.Value);

        _navigationService.NavigateToView<DashboardView>();
    }

    [RelayCommand]
    public async Task RollbackWarrant()
    {
        if (Warrant.PreviousStepId is null)
        {
            return;
        }

        await _warrantService.RollbackWarrant(
            Warrant.Id,
            Warrant.PreviousStepId.Value);

        _navigationService.NavigateToView<DashboardView>();
    }
}
