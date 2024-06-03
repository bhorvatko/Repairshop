using Repairshop.Client.Features.WarrantManagement.WarrantLog;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantLogService
{
    Task<IReadOnlyCollection<WarrantLogEntryViewModel>> GetWarrantLogEntries();
}
