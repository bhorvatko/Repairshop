using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repairshop.Server.Features.WarrantManagement.Data;
using Serilog;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Common;
internal class ApiFactory
    : WebApplicationFactory<Host.Program>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly string _connectionString;

    public ApiFactory(
        ITestOutputHelper testOutputHelper,
        string connectionString)
    {
        _testOutputHelper = testOutputHelper;
        _connectionString = connectionString;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(builder =>
        {
            builder.AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["ApiKey:ValidKeys:0"] = TestConstants.ApiKey,
                ["Database:ConnectionString"] = _connectionString
            })
            .Build();
        });

        builder.UseSerilog((ctx, config) =>
        {
            config.WriteTo.TestOutput(_testOutputHelper);
        });

        IHost host = base.CreateHost(builder);

        using (var scope = host.Services.CreateScope())
        {
            WarrantManagementDbContext dbContext = scope.ServiceProvider.GetRequiredService<WarrantManagementDbContext>();

            dbContext.Database.EnsureCreated();
        }

        return host;
    }
}
