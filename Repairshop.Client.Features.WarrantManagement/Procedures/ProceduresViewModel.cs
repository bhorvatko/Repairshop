using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using System.Drawing;
using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public partial class ProceduresViewModel
    : ViewModelBase
{
    private readonly IProcedureService _procedureService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    [ObservableProperty]
    private IReadOnlyCollection<ProcedureViewModel> _procedures = 
        new List<ProcedureViewModel>();

    private Visibility _editProcedureVisibility = Visibility.Collapsed;
    private ProcedureViewModel? _selectedProcedure;

    public ProceduresViewModel(
        IProcedureService procedureService,
        ILoadingIndicatorService loadingIndicatorService)
    {
        _procedureService = procedureService;
        _loadingIndicatorService = loadingIndicatorService;
    }

    public Visibility EditProcedureVisibility { get => _editProcedureVisibility; set => SetProperty(ref _editProcedureVisibility, value); }

    public ProcedureViewModel? SelectedProcedure
    {
        get => _selectedProcedure;
        set
        {
            SetProperty(ref _selectedProcedure, value);

            EditProcedureVisibility = (_selectedProcedure is not null).ToVisibility();
        }
    }

    [RelayCommand]
    public async Task AddNewProcedure()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            ProcedureViewModel newProcedure = ProcedureViewModel.CreateNew();

            Guid newProcedureId = await _procedureService.CreateProcedure(
                newProcedure.Name,
                ColorToRgb(newProcedure.BackgroundColor));

            newProcedure.SetId(newProcedureId);

            Procedures = Procedures.Concat(new[] { newProcedure }).ToList();
        });
    }

    [RelayCommand]
    public async Task DeleteProcedure(ProcedureViewModel procedure)
    {
        if (SelectedProcedure is null || SelectedProcedure.Id is null)
        {
            return;
        }

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _procedureService.DeleteProcedure(SelectedProcedure.Id.Value);
            Procedures = Procedures.Where(p => p.Id != SelectedProcedure.Id).ToList();
        });
    }

    [RelayCommand]
    public async Task SaveProcedure(ProcedureViewModel procedure)
    {
        if (SelectedProcedure is null || SelectedProcedure.Id is null)
        {
            return;
        }

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _procedureService.UpdateProcedure(
                SelectedProcedure.Id.Value,
                SelectedProcedure.Name,
                ColorToRgb(SelectedProcedure.BackgroundColor));
        });
    }

    [RelayCommand]
    public async Task LoadProcedures()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            Procedures = await _procedureService.GetProcedures();
        });
    }

    private static string ColorToRgb(System.Windows.Media.Color color) =>
        ColorTranslator.ToHtml(
            Color.FromArgb(color.R, color.G, color.B))
                .Replace("#", "");
}
