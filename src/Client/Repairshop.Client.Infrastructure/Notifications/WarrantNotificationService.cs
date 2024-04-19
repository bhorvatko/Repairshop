using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Repairshop.Client.Infrastructure.Notifications;

internal class WarrantNotificationService
    : IWarrantNotificationService
{
    private readonly Subject<WarrantCreatedNotification> _warrantAddedSubject;

    public WarrantNotificationService(IOptions<ApiOptions> apiOptions)
    {
        _warrantAddedSubject = new Subject<WarrantCreatedNotification>();

        HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl(
                "https://localhost/Notifications",
                options =>
                {
                    options.Headers.Add("X-API-KEY", apiOptions.Value.ApiKey);
                })
            .Build();

        hubConnection.On<WarrantCreatedNotification>(
            "RecieveNotification",
            x => _warrantAddedSubject.OnNext(x));
    }

    public IDisposable SubscribeToWarrantAddedNotifications(
        Guid? technicianId,
        Action<WarrantSummaryViewModel> onWarrantAdded) =>
        _warrantAddedSubject
            .Where(x => x.TechnicianId == technicianId)
            .Select(x => x.WarrantModel.ToViewModel())
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
