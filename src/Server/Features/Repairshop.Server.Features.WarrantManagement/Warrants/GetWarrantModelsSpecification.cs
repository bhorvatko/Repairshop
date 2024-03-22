using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal class GetWarrantModelsSpecification
    : Specification<Warrant, WarrantModel>
{
    public GetWarrantModelsSpecification(Guid? technicianId)
    {
        Query.Where(x => x.TechnicianId == technicianId);
        
        Query.Select(x => new WarrantModel()
        {
            Id = x.Id,
            Deadline = x.Deadline,
            IsUrgent = x.IsUrgent,
            TechnicianId = x.TechnicianId,
            Title = x.Title,
            Procedure = new ProcedureModel()
            {
                Id = x.CurrentStep!.Procedure.Id,
                Color = x.CurrentStep!.Procedure.Color,
                Name = x.CurrentStep!.Procedure.Name
            }
        });
    }
}
