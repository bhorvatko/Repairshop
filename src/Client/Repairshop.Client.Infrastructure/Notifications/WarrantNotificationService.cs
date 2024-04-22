using Microsoft.AspNetCore.SignalR.Client;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Infrastructure.Services;
using Repairshop.Shared.Features.WarrantManagement.Technicians;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Repairshop.Client.Infrastructure.Notifications;

internal class WarrantNotificationService
    : IWarrantNotificationService
{
    private readonly Subject<WarrantCreatedNotification> _warrantAddedSubject;
    private readonly Subject<WarrantAssignedNotification> _warrantAssignedSubject;
    private readonly WarrantSummaryViewModelFactory _warrantSummaryViewModelFactory;
    private readonly HubConnection _hubConnection;

    public WarrantNotificationService(
        WarrantSummaryViewModelFactory warrantSummaryViewModelFactory,
        HubConnection hubConnection)
    {
        _warrantSummaryViewModelFactory = warrantSummaryViewModelFactory;
        _hubConnection = hubConnection;

        _warrantAddedSubject = CreateSubject<WarrantCreatedNotification>();
        _warrantAssignedSubject = CreateSubject<WarrantAssignedNotification>();

        _hubConnection.StartAsync().Wait();
    }

    public IDisposable SubscribeToWarrantAddedNotifications(
        Guid? technicianId,
        Action<WarrantSummaryViewModel> onWarrantAdded)
    {
        IObservable<WarrantSummaryViewModel> warrantAssignedObservable =
            _warrantAssignedSubject
                .Where(x => x.ToTechnicianId == technicianId)
                .Select(x => _warrantSummaryViewModelFactory.Create(x.Warrant));

        IObservable<WarrantSummaryViewModel> warrantCreatedObservable =
            _warrantAddedSubject.Select(x => _warrantSummaryViewModelFactory.Create(x.Warrant));

        IObservable<WarrantSummaryViewModel> resultingObservable = 
            technicianId is null
                ? Observable.Merge(warrantAssignedObservable, warrantCreatedObservable)
                : warrantAssignedObservable;

        return resultingObservable.Subscribe(onWarrantAdded);
    }

    public IDisposable SubscribeToWarrantRemovedNotifications(
        Guid? technicianId, 
        Action<Guid> onWarrantRemoved)
    {
        return _warrantAssignedSubject
            .Where(x => x.FromTechnicianId == technicianId)
            .Select(x => x.Warrant.Id)
            .Subscribe(onWarrantRemoved);
    }

    public IDisposable SubscribeToWarrantUpdatedNotifications(Guid? technicianId, Action<WarrantSummaryViewModel> onWarrantUpdated)
    {
        throw new NotImplementedException();
    }

    private Subject<TSubject> CreateSubject<TSubject>()
    {
        Subject<TSubject> subject = new Subject<TSubject>();

        _hubConnection.On<TSubject>(
            typeof(TSubject).Name,
            subject.OnNext);

        return subject;
    }
}
