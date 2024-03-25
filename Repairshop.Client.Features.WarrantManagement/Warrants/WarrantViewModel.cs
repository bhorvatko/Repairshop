using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantViewModel
{
    public WarrantViewModel(
        Guid id, 
        bool isUrgent, 
        DateTime deadline,  
        string title, 
        IEnumerable<WarrantStep> steps)
    {
        Id = id;
        IsUrgent = isUrgent;
        Deadline = deadline;
        Title = title;
        Steps = steps;
    }

    public Guid Id { get; private set; }
    public bool IsUrgent { get; private set; }
    public DateTime Deadline { get; private set; }
    public string Title { get; private set; }
    public IEnumerable<WarrantStep> Steps { get; private set; }

    public static WarrantViewModel Create(
        Guid id,
        bool isUrgent,
        DateTime deadline,
        Procedure procedure,
        string title,
        IEnumerable<WarrantStep> steps)
    {
        return new WarrantViewModel(
            id,
            isUrgent,
            deadline,
            title,
            steps);
    }
}
