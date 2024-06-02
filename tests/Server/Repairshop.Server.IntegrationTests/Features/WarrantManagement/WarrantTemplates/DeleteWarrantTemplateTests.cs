using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.WarrantTemplates;

public class DeleteWarrantTemplateTests
    : IntegrationTestBase
{
    public DeleteWarrantTemplateTests(
        ITestOutputHelper testOutputHelper,
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Deleting_a_warrant_template()
    {
        // Arrange
        WarrantTemplate warrantTemplate = await WarrantTemplateHelper.Create();

        _dbContext.Add(warrantTemplate);
        _dbContext.SaveChanges();

        // Act
        await _client.DeleteAsync($"WarrantTemplates/{warrantTemplate.Id}");

        // Assert
        IReadOnlyCollection<WarrantTemplate> warrantTemplates =
            await _dbContext.Set<WarrantTemplate>().AsNoTracking().ToListAsync();

        warrantTemplates.Should().BeEmpty();

        IReadOnlyCollection<WarrantTemplateStep> warrantTemplateSteps =
            await _dbContext.Set<WarrantTemplateStep>().AsNoTracking().ToListAsync();

        warrantTemplateSteps.Should().BeEmpty();
    }
}
