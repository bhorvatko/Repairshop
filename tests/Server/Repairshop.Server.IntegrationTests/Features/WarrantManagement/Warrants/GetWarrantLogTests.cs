using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Warrants.GetWarrantLog;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class GetWarrantLogTests
    : IntegrationTestBase
{
    public GetWarrantLogTests(
        ITestOutputHelper testOutputHelper,
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Getting_the_warrant_log()
    {
        // Arrange
        DateTimeOffset eventTime = DateTimeOffset.UtcNow;

        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(
            _dbContext,
            getUtcNow: () => eventTime);

        GetWarrantLogRequest request = new();

        // Act
        GetWarrantLogResponse response = 
            (await _client.GetFromJsonAsync<GetWarrantLogResponse>("Warrants/Log", request))!;

        // Assert
        response.LogEntries.Single().Should().Match<WarrantLogEntryModel>(
            x => x.EventTime == eventTime
                && x.WarrantNumber == warrant.Number
                && x.PreviousState == null
                && x.NewState == warrant.Steps.First().Procedure.Name
                && x.TechnicianName == null);
    }
}
