using Repairshop.Server.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;
public static class TechnicianHelper
{
    public static Technician Create(
        string name = "John") =>
        Technician.Create(name);
}
