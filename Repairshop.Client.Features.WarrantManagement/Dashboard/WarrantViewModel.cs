using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public class WarrantViewModel
{
    private WarrantViewModel(
        bool isUrgent,
        DateTime deadline,
        Procedure procedure,
        string title,
        bool canBeRolledBack,
        bool canBeAdvanced) 
    {
        IsUrgent = isUrgent;
        Deadline = deadline;
        Procedure = procedure;
        Title = title;
        CanBeRolledBack = canBeRolledBack;
        CanBeAdvanced = canBeAdvanced;
    }

    public bool IsUrgent { get; private set; }
    public DateTime Deadline { get; private set; }
    public Procedure Procedure { get; private set; }
    public string Title { get; private set; }
    public bool CanBeRolledBack { get; private set; }
    public bool CanBeAdvanced { get; private set; }

    public static WarrantViewModel Create(
        bool isUrgent,
        DateTime deadline,
        Procedure procedure,
        string title,
        bool canBeRolledBack,
        bool canBeAdvanced) =>
        new WarrantViewModel(
            isUrgent,
            deadline,
            procedure,
            title,
            canBeRolledBack,
            canBeAdvanced);

}
