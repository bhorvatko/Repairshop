using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public class WarrantSummaryViewModel
{
    private WarrantSummaryViewModel(
        Guid id,
        bool isUrgent,
        DateTime deadline,
        ProcedureSummaryViewModel procedure,
        string title,
        int number,
        bool canBeRolledBack,
        bool canBeAdvanced,
        Guid? nextStepId,
        Guid? previousStepId,
        Guid? technicianId) 
    {
        Id = id;
        IsUrgent = isUrgent;
        Deadline = deadline;
        Procedure = procedure;
        Title = title;
        Number = number;
        CanBeRolledBack = canBeRolledBack;
        CanBeAdvanced = canBeAdvanced;
        NextStepId = nextStepId;
        PreviousStepId = previousStepId;
        TechnicianId = technicianId;
    }

    public Guid Id { get; private set; }
    public bool IsUrgent { get; private set; }
    public DateTime Deadline { get; private set; }
    public ProcedureSummaryViewModel Procedure { get; private set; }
    public string Title { get; private set; }
    public int Number { get; private set; }
    public bool CanBeRolledBack { get; private set; }
    public bool CanBeAdvanced { get; private set; }
    public Guid? NextStepId { get; private set; }
    public Guid? PreviousStepId { get; private set; }
    public Guid? TechnicianId { get; private set; }

    public Visibility CanBeRolledBackVisibility => 
        (CanBeRolledBack && PreviousStepId is not null).ToVisibility();

    public Visibility CanBeAdvancedVisibility => 
        (CanBeAdvanced && NextStepId is not null).ToVisibility();

    public string NumberString => "RN" + Number.ToString();

    public static WarrantSummaryViewModel Create(
        Guid id,
        bool isUrgent,
        DateTime deadline,
        ProcedureSummaryViewModel procedure,
        string title,
        int number,
        bool canBeRolledBack,
        bool canBeAdvanced,
        Guid? nextStepId,
        Guid? previousStepId,
        Guid? technicianId) =>
        new WarrantSummaryViewModel(
            id,
            isUrgent,
            deadline,
            procedure,
            title,
            number,
            canBeRolledBack,
            canBeAdvanced,
            nextStepId,
            previousStepId,
            technicianId);

}
