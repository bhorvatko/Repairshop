using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.WarrantLog;
using Repairshop.Shared.Features.WarrantManagement.Warrants.GetWarrantLog;

namespace Repairshop.Client.Infrastructure.Services;

internal class WarrantLogService
    : IWarrantLogService
{
    private readonly ApiClient.ApiClient _apiClient;

    public WarrantLogService(ApiClient.ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IReadOnlyCollection<WarrantLogEntryViewModel>> GetWarrantLogEntries()
    {
        GetWarrantLogRequest request = new();

        GetWarrantLogResponse response = 
            await _apiClient.Get<GetWarrantLogResponse>(
                "Warrants/Log",
                request);

        return response
            .LogEntries
            .Select(x => new WarrantLogEntryViewModel()
            {
                EventDescription = string.IsNullOrEmpty(x.PreviousState)
                    ? $"Dodan je nalog {x.WarrantNumber} sa stanjem {x.NewState}"
                    : $"Nalog {x.WarrantNumber} je prešao iz stanja {x.PreviousState} u stanje {x.NewState}",
                EventTime = x.EventTime.LocalDateTime,
                TechnicianName = x.TechnicianName,
                WarrantNumber = x.WarrantNumber,
            })
            .ToList();
    }
}
