using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantNotificationService
{
    Task<IDisposable> SubscribeToWarrantAddedNotifications(
        Guid? technicianId, 
        Action<WarrantSummaryViewModel> onWarrantAdded);

    Task<IDisposable> SubscribeToWarrantRemovedNotifications(
        Guid? technicianId, 
        Action<Guid> onWarrantRemoved);

    Task<IDisposable> SubscribeToWarrantUpdatedNotifications(
        Guid? technicianId, 
        Action<WarrantSummaryViewModel> onWarrantUpdated);
}
