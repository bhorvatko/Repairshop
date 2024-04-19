using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantNotificationService
{
    IDisposable SubscribeToWarrantAddedNotifications(
        Guid? technicianId, 
        Action<WarrantSummaryViewModel> onWarrantAdded);

    IDisposable SubscribeToWarrantRemovedNotifications(
        Guid? technicianId, 
        Action<Guid> onWarrantRemoved);

    IDisposable SubscribeToWarrantUpdatedNotifications(
        Guid? technicianId, 
        Action<WarrantSummaryViewModel> onWarrantUpdated);
}
