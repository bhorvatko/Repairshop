﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public partial class WarrantTemplateMaintenanceViewModel
    : ViewModelBase
{
    private readonly IWarrantTemplateService _warrantTemplateService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IFormService _formService;

    [ObservableProperty]
    private IReadOnlyCollection<WarrantTemplateViewModel> _warrantTemplates =
        new List<WarrantTemplateViewModel>();

    public WarrantTemplateMaintenanceViewModel(
        IWarrantTemplateService warrantTemplateService,
        ILoadingIndicatorService loadingIndicatorService,
        IFormService formService)
    {
        _warrantTemplateService = warrantTemplateService;
        _loadingIndicatorService = loadingIndicatorService;
        _formService = formService;
    }

    [RelayCommand]
    private async Task OnLoaded()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(LoadWarrantTemplates);
    }

    [RelayCommand]
    private async Task AddWarrantTemplate()
    {
        await _formService.ShowFormAsDialog<CreateWarrantTemplateView>();

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(LoadWarrantTemplates);
    }

    [RelayCommand]
    private async Task EditWarrantTemplate(WarrantTemplateViewModel warrantTemplate)
    {
        await _formService.ShowFormAsDialog<
            UpdateWarrantTemplateView,
            UpdateWarrantTemplateViewModel>(vm =>
            {
                vm.WarrantTemplateId = warrantTemplate.Id;
                vm.EditWarrantTemplateViewModel.Name = warrantTemplate.Name;
                vm.EditWarrantTemplateViewModel.Steps = 
                    warrantTemplate
                        .Steps
                        .Select(s => WarrantStep.Create(
                            s.Procedure, 
                            s.CanBeTransitionedToByFrontDesk, 
                            s.CanBeTransitionedToByWorkshop));
            });

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(LoadWarrantTemplates);
    }

    [RelayCommand]
    private void DeleteWarrantTemplate(WarrantTemplateViewModel warrantTemplate)
    {
        throw new NotImplementedException();
    }

    private async Task LoadWarrantTemplates()
    {
        WarrantTemplates = await _warrantTemplateService.GetWarrantTemplates();
    }
}
