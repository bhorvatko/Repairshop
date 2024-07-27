using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public class ProcedureListRowViewModel
{
    public required ProcedureViewModel Procedure { get; init; }
    public Visibility MoveUpButtonVisibility { get; init; }
    public Visibility MoveDownButtonVisibility { get; init; }
}
