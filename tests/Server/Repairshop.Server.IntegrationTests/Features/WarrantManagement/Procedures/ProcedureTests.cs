using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Procedures;

public class ProcedureTests
    : IntegrationTestBase
{
    public ProcedureTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Creating_a_procedure_should_result_in_new_entity()
    {
        CreateProcedureRequest request = new()
        {
            Color = "FFFFFF",
            Name = "Name"
        };

        await _client.PostAsJsonAsync("Procedures", request);

        Procedure createdEntity = _dbContext.Set<Procedure>().Single();

        createdEntity.Id.Should().NotBeEmpty();
        createdEntity.Color.Value.Should().Be("FFFFFF");
        createdEntity.Name.Should().Be("Name");
    }

    [Fact]
    public async Task Get_procedure_summaries()
    {
        _dbContext.Add(ProcedureHelper.Create(name: "P1", color: "000000"));
        _dbContext.SaveChanges();

        GetProcedureSummariesResponse response = 
            (await _client.GetFromJsonAsync<GetProcedureSummariesResponse>("Procedures/Summaries"))!;

        response.Procedures.Should().HaveCount(1);
        response.Procedures.Single().Name.Should().Be("P1");
        response.Procedures.Single().Color.Should().Be("000000");
    }

    [Fact]
    public async Task Updating_a_procedure_should_result_in_an_updated_entity()
    {
        // Arrange
        Procedure procedure = ProcedureHelper.Create();

        _dbContext.Add(procedure);
        _dbContext.SaveChanges();

        UpdateProcedureRequest request = new UpdateProcedureRequest()
        {
            Id = procedure.Id,
            Name = "new",
            Color = "123456"
        };

        // Act
        await _client.PutAsJsonAsync("Procedures", request);

        // Assert
        Procedure updatedProcedure = _dbContext.Set<Procedure>().Single();
        _dbContext.Entry(updatedProcedure).Reload();

        updatedProcedure.Id.Should().Be(request.Id);
        updatedProcedure.Name.Should().Be("new");
        updatedProcedure.Color.Value.Should().Be("123456");
    }

    [Fact]
    public async Task Deleting_a_procedure()
    {
        // Arrange
        Procedure procedure = ProcedureHelper.Create();

        _dbContext.Add(procedure);
        _dbContext.SaveChanges(); 

        IEnumerable<Procedure> proceduresBeforeAct =
            _dbContext.Set<Procedure>().AsNoTracking().ToList();

        // Act
        await _client.DeleteAsync($"Procedures/{procedure.Id}");

        // Assert
        IEnumerable<Procedure> proceduresAfterAct =
            _dbContext.Set<Procedure>().AsNoTracking().ToList();

        proceduresBeforeAct.Should().HaveCount(1);
        proceduresAfterAct.Should().BeEmpty();
    }

    [Fact]
    public async Task Get_procedures()
    {
        // Arrange
        Procedure procedure = ProcedureHelper.Create();

        IEnumerable<WarrantStep> warrantSteps = 
            await WarrantStepHelper.CreateStepSequence(new[] { procedure });

        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(
            _dbContext,
            steps: warrantSteps);

        IEnumerable<WarrantTemplateStep> templateSteps =
            await WarrantTemplateStepHelper.CreateStepSequence(new[] { procedure });

        WarrantTemplate warrantTemplate =
            await WarrantTemplateHelper.Create(steps: templateSteps);

        _dbContext.Add(warrantTemplate);
        _dbContext.SaveChanges();

        // Act
        GetProceduresResponse response =
            (await _client.GetFromJsonAsync<GetProceduresResponse>("Procedures"))!;

        // Assert
        response.Procedures.Should().HaveCount(1);
        response.Procedures.Single().Name.Should().Be(procedure.Name);
        response.Procedures.Single().Color.Should().Be(procedure.Color);

        response.Procedures.Single()
            .UsedByWarrants
            .Should().BeEquivalentTo(new[] { warrant.Title });

        response.Procedures.Single()
            .UsedByWarrantTemplates
            .Should().BeEquivalentTo(new[] { warrantTemplate.Name });
    }
}
