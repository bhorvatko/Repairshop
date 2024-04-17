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

    private IEnumerable<Procedure> _procedures = Enumerable.Empty<Procedure>();
    private Visibility _editProcedureVisibility = Visibility.Collapsed;
    private Procedure? _selectedProcedure;

    public ProceduresViewModel(
        IProcedureService procedureService,
        ILoadingIndicatorService loadingIndicatorService)
    {
        _procedureService = procedureService;
        _loadingIndicatorService = loadingIndicatorService;
    }

    public IEnumerable<Procedure> Procedures { get => _procedures; set => SetProperty(ref _procedures, value); }
    public Visibility EditProcedureVisibility { get => _editProcedureVisibility; set => SetProperty(ref _editProcedureVisibility, value); }

    public Procedure? SelectedProcedure
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
            Procedure newProcedure = Procedure.CreateNew();

            await _procedureService.CreateProcedure(
                newProcedure.Name,
                ColorToRgb(newProcedure.BackgroundColor));

            Procedures = Procedures.Concat(new[] { newProcedure });
        });
    }

    [RelayCommand]
    public Task DeleteProcedure(Procedure procedure)
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    public async Task SaveProcedure(Procedure procedure)
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
