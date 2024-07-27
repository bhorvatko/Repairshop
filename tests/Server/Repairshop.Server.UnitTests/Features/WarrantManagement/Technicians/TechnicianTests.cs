using FluentAssertions;
using Repairshop.Server.Common.Exceptions;
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

    [Fact]
    public async Task Assigning_a_warrant_already_assigned_to_the_technician_should_fail()
    {
        Technician technician = Technician.Create("Test");
        Warrant warrant = await WarrantHelper.Create();
        technician.AssignWarrant(warrant);

        Action act = () => technician.AssignWarrant(warrant);

        act.Should()
            .Throw<DomainArgumentException>()
            .Where(x => x.InvalidArgument == warrant);
    }

    [Fact]
    public async Task Unassigning_all_warrants()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create();
        Warrant warrant = await WarrantHelper.Create();

        technician.AssignWarrant(warrant);

        // Act
        technician.UnassignAllWarrants();

        // Assert
        technician.Warrants.Should().BeEmpty();
        warrant.Technician.Should().BeNull();
        warrant.TechnicianId.Should().BeNull();
    }
}
