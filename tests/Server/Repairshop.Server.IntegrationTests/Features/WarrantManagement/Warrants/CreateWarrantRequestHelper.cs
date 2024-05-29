using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;
internal static class CreateWarrantRequestHelper
{
    public static CreateWarrantRequest Create(
        IEnumerable<WarrantStepDto> steps,
        string title = "Title",
        DateTime? deadline = null,
        bool isUrgent = false) =>
        new CreateWarrantRequest()
        {
            Title = title,
            Deadline = deadline ?? DateTime.MaxValue,
            IsUrgnet = isUrgent,
            Number = 1,
            Steps = steps
        };
}
