using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public partial class WarrantTemplateSelectorViewModel
    : DialogViewModelBase<IEnumerable<WarrantStep>, WarrantTemplateSelectorView>
{
    private readonly IWarrantTemplateService _warrantTemplateService;

    [ObservableProperty]
    private IEnumerable<WarrantTemplateViewModel>? _warrantTemplates;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ApplyButtonEnabled))]
    private WarrantTemplateViewModel? _selectedWarrantTemplate;

    public WarrantTemplateSelectorViewModel(IWarrantTemplateService warrantTemplateService)
    {
        _warrantTemplateService = warrantTemplateService;
    }

    public bool ApplyButtonEnabled => SelectedWarrantTemplate is not null;

    [RelayCommand]
    public async Task OnLoaded()
    {
        WarrantTemplates = await _warrantTemplateService.GetWarrantTemplates();
    }

    [RelayCommand]
    public void Apply()
    {
        if (SelectedWarrantTemplate is null) return;

        FinishDialog(SelectedWarrantTemplate.Steps);
    }

    [RelayCommand]
    public void Cancel()
    {
        CancelDialog();
    }
}
