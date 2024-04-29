using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantNotificationService
{
    Task<IDisposable> SubscribeToWarrantAddedNotifications(
        Guid? technicianId, 
        Func<WarrantSummaryViewModel, Task> onWarrantAdded);

    Task<IDisposable> SubscribeToWarrantRemovedNotifications(
        Guid? technicianId, 
        Func<Guid, Task> onWarrantRemoved);

    Task<IDisposable> SubscribeToWarrantUpdatedNotifications(
        Guid? technicianId, 
        Func<WarrantSummaryViewModel, Task> onWarrantUpdated);
}
