namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantTemplateService
{
    Task CreateWarrantTemplate(
        string name,
        IEnumerable<CreateWarrantStepDto> steps);
}
