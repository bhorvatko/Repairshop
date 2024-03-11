using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.Technicians;
public class TechnicianTests
{
    [Fact]
    public void Creating_a_valid_technician()
    {
        Technician technician = Technician.Create("Test");

        technician.Name.Should().Be("Test");
    }

    [Fact]
    public async Task Assigning_a_warrant()
    {
        Technician technician = Technician.Create("Test");
        Warrant warrant = await WarrantHelper.Create();

        technician.AssignWarrant(warrant);

        technician.Warrants.Should().HaveCount(1);
    }
}
