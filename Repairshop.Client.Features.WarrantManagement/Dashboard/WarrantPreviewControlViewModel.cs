﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.ClientContext;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Common.ClientContext;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class WarrantPreviewControlViewModel
    : ObservableObject, IDisposable
{
    private static AnimationClock _animationClock =
        new AnimationClock(5);

    private readonly INavigationService _navigationService;
    private readonly IWarrantService _warrantService;
    private readonly IClientContextProvider _clientContextProvider;
    private readonly IFormService _formService;

    public WarrantPreviewControlViewModel(
        WarrantSummaryViewModel warrant,
        INavigationService navigationService,
        IWarrantService warrantService,
        IClientContextProvider clientContextProvider,
        IFormService formService)
    {
        Warrant = warrant;
        _navigationService = navigationService;
        _warrantService = warrantService;
        _clientContextProvider = clientContextProvider;
        this._formService = formService;
        _animationClock.Tick += UpdateLabelContent;
    }

    public WarrantSummaryViewModel Warrant { get; set; }
    public bool PlayUpdateAnimation { get; set; }

    public string DeadlineDescription =>
        Warrant.IsUrgent ? "Hitni nalog" : Warrant.Deadline.ToString("dd.MM HH:mm");

    public string LabelContent => 
        _animationClock.State ? Warrant.Title : DeadlineDescription;

    [RelayCommand]
    public async Task UpdateWarrant()
    {
        // Only Front office can edit warrants
        if (_clientContextProvider.GetClientContext() != RepairshopClientContext.FrontOffice)
        {
            return;
        }

        WarrantViewModel warrant =
            await _warrantService.GetWarrant(Warrant.Id);

        await _formService.ShowFormAsDialog<UpdateWarrantView, UpdateWarrantViewModel>(vm =>
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

    [RelayCommand]
    public void StartDrag(MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            WarrantPreviewControl source = (WarrantPreviewControl)e.Source;

            WarrantPreviewControlViewModel? sourceViewModel = 
                source.DataContext as WarrantPreviewControlViewModel;

            if (sourceViewModel is null) return;

            DragDrop.DoDragDrop(source, sourceViewModel.Warrant, DragDropEffects.Move);
        }
    }

    public void Dispose()
    {
        _animationClock.Tick -= UpdateLabelContent;
    }

    private void UpdateLabelContent(object? sender, EventArgs eventArgs)
    {
        OnPropertyChanged(nameof(LabelContent));
    }

    private class AnimationClock : DispatcherTimer
    {
        public bool State { get; private set; } = true;

        public AnimationClock(int intervalInSeconds)
        {
            Interval = new TimeSpan(0, 0, intervalInSeconds);

            Tick += (s, e) => State = !State;

            Start();
        }
    }
}
