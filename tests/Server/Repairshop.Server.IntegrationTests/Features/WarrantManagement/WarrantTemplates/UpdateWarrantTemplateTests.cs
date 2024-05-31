using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.WarrantTemplates;

public class UpdateWarrantTemplateTests
    : IntegrationTestBase
{
    public UpdateWarrantTemplateTests(
        ITestOutputHelper testOutputHelper,
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Upating_a_warrant_template()
    {
        // Arrange
        WarrantTemplate warrantTemplate =
            await WarrantTemplateHelper.Create();

        _dbContext.Add(warrantTemplate);
        _dbContext.SaveChanges();

        UpdateWarrantTemplateRequest request = new()
        {
            Id = warrantTemplate.Id,
            Name = "Updated Template",
            Steps = warrantTemplate
                .Steps
                .Take(2)
                .Select(x => WarrantStepDtoHelper.Create(x.ProcedureId))
                .ToList()
        };
            
        // Act
        await _client.PutAsJsonAsync("WarrantTemplates", request);

        // Assert
        WarrantTemplate updatedWarrantTemplate =
            _dbContext
                .Set<WarrantTemplate>()
                .AsNoTracking()
                .Include(x => x.Steps)
                .Single();

        updatedWarrantTemplate.Name.Should().Be(request.Name);
        updatedWarrantTemplate.Steps.Count().Should().Be(request.Steps.Count);

        updatedWarrantTemplate
            .Steps
            .Select(x => x.ProcedureId)
            .Should()
            .BeEquivalentTo(request.Steps.Select(x => x.ProcedureId));
    }
}
