namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantViewModel
{
    private WarrantViewModel(
        Guid id, 
        bool isUrgent, 
        DateTime deadline,  
        string title, 
        int number,
        Guid procedureId,
        IEnumerable<WarrantStep> steps)
    {
        Id = id;
        IsUrgent = isUrgent;
        Deadline = deadline;
        Title = title;
        Number = number;
        ProcedureId = procedureId;
        Steps = steps;
    }

    public Guid Id { get; private set; }
    public bool IsUrgent { get; private set; }
    public DateTime Deadline { get; private set; }
    public string Title { get; private set; }
    public int Number { get; private set; }
    public Guid ProcedureId { get; private set; }
    public IEnumerable<WarrantStep> Steps { get; private set; }

    public static WarrantViewModel Create(
        Guid id,
        bool isUrgent,
        DateTime deadline,
        string title,
        int number,
        Guid procedureId,
        IEnumerable<WarrantStep> steps)
    {
        return new WarrantViewModel(
            id,
            isUrgent,
            deadline,
            title,
            number,
            procedureId,
            steps);
    }
}
