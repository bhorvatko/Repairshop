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
        Procedure procedure,
        string title,
        bool canBeRolledBack,
        bool canBeAdvanced,
        Guid? nextStepId,
        Guid? previousStepId) 
    {
        Id = id;
        IsUrgent = isUrgent;
        Deadline = deadline;
        Procedure = procedure;
        Title = title;
        CanBeRolledBack = canBeRolledBack;
        CanBeAdvanced = canBeAdvanced;
        NextStepId = nextStepId;
        PreviousStepId = previousStepId;
    }

    public Guid Id { get; private set; }
    public bool IsUrgent { get; private set; }
    public DateTime Deadline { get; private set; }
    public Procedure Procedure { get; private set; }
    public string Title { get; private set; }
    public bool CanBeRolledBack { get; private set; }
    public bool CanBeAdvanced { get; private set; }
    public Guid? NextStepId { get; private set; }
    public Guid? PreviousStepId { get; private set; }

    public Visibility CanBeRolledBackVisibility => 
        (CanBeRolledBack && PreviousStepId is not null).ToVisibility();

    public Visibility CanBeAdvancedVisibility => 
        (CanBeAdvanced && NextStepId is not null).ToVisibility();

    public static WarrantSummaryViewModel Create(
        Guid id,
        bool isUrgent,
        DateTime deadline,
        Procedure procedure,
        string title,
        bool canBeRolledBack,
        bool canBeAdvanced,
        Guid? nextStepId,
        Guid? previousStepId) =>
        new WarrantSummaryViewModel(
            id,
            isUrgent,
            deadline,
            procedure,
            title,
            canBeRolledBack,
            canBeAdvanced,
            nextStepId,
            previousStepId);

}
