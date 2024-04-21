using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Client.Infrastructure.Services;
using Repairshop.Shared.Common.Notifications;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Repairshop.Client.Infrastructure.Notifications;

internal class WarrantNotificationService
    : IWarrantNotificationService
{
    private readonly Subject<WarrantCreatedNotification> _warrantAddedSubject;
    private readonly WarrantSummaryViewModelFactory _warrantSummaryViewModelFactory;

    public WarrantNotificationService(
        IOptions<ApiOptions> apiOptions,
        WarrantSummaryViewModelFactory warrantSummaryViewModelFactory)
    {
        _warrantSummaryViewModelFactory = warrantSummaryViewModelFactory;

        _warrantAddedSubject = new Subject<WarrantCreatedNotification>();

        HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl(
                $"{apiOptions.Value.BaseAddress}/{NotificationConstants.NotificationsEndpoint}",
                options =>
                {
                    options.Headers.Add("X-API-KEY", apiOptions.Value.ApiKey);
                })
            .Build();

        hubConnection.On<WarrantCreatedNotification>(
            NotificationConstants.ReceiveNotificationMethodName,
            x => _warrantAddedSubject.OnNext(x));
    }

    public IDisposable SubscribeToWarrantAddedNotifications(
        Guid? technicianId,
        Action<WarrantSummaryViewModel> onWarrantAdded) =>
        // TO DO: Subscribe only if technicianId is null
        _warrantAddedSubject
            .Select(x => _warrantSummaryViewModelFactory.Create(x.WarrantModel))
            .Subscribe(onWarrantAdded);

    public IDisposable SubscribeToWarrantRemovedNotifications(Guid? technicianId, Action<Guid> onWarrantRemoved)
    {
        throw new NotImplementedException();
    }

    public IDisposable SubscribeToWarrantUpdatedNotifications(Guid? technicianId, Action<WarrantSummaryViewModel> onWarrantUpdated)
    {
        throw new NotImplementedException();
    }
}
