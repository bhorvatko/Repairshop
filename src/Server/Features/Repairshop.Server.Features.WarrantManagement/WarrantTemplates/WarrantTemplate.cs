using Repairshop.Server.Common.Entities;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplate
    : EntityBase
{
#nullable disable
    private WarrantTemplate() { }
#nullable enable

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<WarrantTemplateStep> Steps { get; private set; }

    public static WarrantTemplate Create(
        string name,
        IEnumerable<WarrantTemplateStep> steps)
    {
        return new WarrantTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Steps = steps.ToList()
        };
    }
}
