using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Client.Infrastructure.Services;
using Repairshop.Shared.Common.Notifications;
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
        IOptions<ApiOptions> apiOptions,
        WarrantSummaryViewModelFactory warrantSummaryViewModelFactory)
    {
        _warrantSummaryViewModelFactory = warrantSummaryViewModelFactory;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(
                $"{apiOptions.Value.BaseAddress}/{NotificationConstants.NotificationsEndpoint}",
                options =>
                {
                    options.Headers.Add("X-API-KEY", apiOptions.Value.ApiKey);
                })
            .Build();

        _warrantAddedSubject = CreateSubject<WarrantCreatedNotification>();
        _warrantAssignedSubject = CreateSubject<WarrantAssignedNotification>();

        _hubConnection.StartAsync().Wait();
    }

    public IDisposable SubscribeToWarrantAddedNotifications(
        Guid? technicianId,
        Action<WarrantSummaryViewModel> onWarrantAdded)
    {
        return _warrantAssignedSubject
            .Where(x => x.ToTechnicianId == technicianId)
            .Select(x => _warrantSummaryViewModelFactory.Create(x.Warrant))
            .Subscribe(onWarrantAdded);

        // TO DO: Subscribe only if technicianId is null
        //_warrantAddedSubject
        //    .Select(x => _warrantSummaryViewModelFactory.Create(x.Warrant))
        //    .Subscribe(onWarrantAdded);
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
