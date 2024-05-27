using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

public class Procedure
{
#nullable disable
    private Procedure() { }
#nullable enable

    private Procedure(
        Guid id,
        string name,
        ColorCode color,
        ProcedurePriority priority)
    {
        Id = id;
        Color = color;
        Name = name;
        Priority = priority;
    }

    public Guid Id { get; private set; }
    public ColorCode Color { get; private set; }
    public string Name { get; private set; }
    public ProcedurePriority Priority { get; private set; }

    public IReadOnlyCollection<WarrantStep> WarrantSteps { get; private set; } =
        new List<WarrantStep>();

    public IReadOnlyCollection<WarrantTemplateStep> WarrantTemplateSteps { get; private set; } =
        new List<WarrantTemplateStep>();

    public static Procedure Create(
        string name,
        ColorCode color,
        ProcedurePriority priority)
    {
        return new Procedure(Guid.NewGuid(), name, color, priority);
    }

    public void Update(
        string name,
        ColorCode color)
    {
        Name = name;
        Color = color;
    }

    public void SetPriority(ProcedurePriority priority)
    {
        Priority = priority;
    }   
}
