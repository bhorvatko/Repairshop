using Testcontainers.MsSql;


namespace Repairshop.Server.IntegrationTests.Common;

public class DatabaseFixture
    : IDisposable
{
    private readonly MsSqlContainer _msSqlContainer;

    public DatabaseFixture()
    {
        _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04")
            .WithCleanUp(true)
            .Build();

        _msSqlContainer.StartAsync().Wait();

        ConnectionString = _msSqlContainer.GetConnectionString();
    }

    public string ConnectionString { get; set; }

    public void Dispose()
    {
        _msSqlContainer.DisposeAsync();
    }
}
