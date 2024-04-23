using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.WarrantTemplates;

public class GetWarrantTemplatesTests : IntegrationTestBase
{
    public GetWarrantTemplatesTests(
        ITestOutputHelper testOutputHelper,
        DatabaseFixture databaseFixture)
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Getting_all_warrant_templates()
    {
        // Arrange
        WarrantTemplate warrantTemplate = await WarrantTemplateHelper.Create();

        _dbContext.Add(warrantTemplate);
        _dbContext.SaveChanges();

        // Act
        GetWarrantTemplatesResponse response = 
            (await _client.GetFromJsonAsync<GetWarrantTemplatesResponse>($"WarrantTemplates"))!;

        // Assert
        response.WarrantTemplates.Should().HaveCount(1);
        response.WarrantTemplates.First().Should().Match<WarrantTemplateModel>(x => 
            x.Name == warrantTemplate.Name
                && x.Steps.Count() == warrantTemplate.Steps.Count());
    }
}
