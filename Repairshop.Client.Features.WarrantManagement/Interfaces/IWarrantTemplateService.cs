using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantTemplateService
{
    Task CreateWarrantTemplate(
        string name,
        IEnumerable<CreateWarrantStepDto> steps);

    Task<IReadOnlyCollection<WarrantTemplateViewModel>> GetWarrantTemplates();
}
