using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantService
{
    Task CreateWarrant(
        string title,
        DateTime deadline,
        bool isUrgent,
        IEnumerable<CreateWarrantStepDto> steps);

    Task<IEnumerable<WarrantViewModel>> GetUnassignedWarrants();
}

public class CreateWarrantStepDto
{
    public required Guid ProcedureId { get; set; }
    public required bool CanBeTransitionedToByFrontDesk { get; set; }
    public required bool CanBeTransitionedToByWorkshop { get; set; }
}
