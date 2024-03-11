using Repairshop.Server.Features.WarrantManagement;
using Repairshop.Server.Infrastructure;
using Repairshop.Server.Infrastructure.Logging;
using Repairshop.Server.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services
    .AddInfrastructure<Program>(configuration)
    .AddWarrantManagement(
        () => configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>()!.ConnectionString,
        new[] { typeof(EventPublishingInterceptor) });

builder.Host.UseLogging();

var app = builder.Build();

app.UseInfrastructure();

app.Run();


namespace Repairshop.Server.Host
{
    public partial class Program
    {
        protected Program() { }
    }
}