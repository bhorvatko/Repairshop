namespace Repairshop.Server.IntegrationTests.Common;

[CollectionDefinition(TestConstants.Collections.IntegrationTests)]
public class IntegrationTestCollection
    : ICollectionFixture<DatabaseFixture>
{
}
