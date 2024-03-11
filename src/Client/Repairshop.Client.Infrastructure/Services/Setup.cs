using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Infrastructure.Services;
internal static class Setup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services
            .AddTransient<IProcedureService, ProcedureService>()
            .AddTransient<ITechnicianService, TechnicianService>();
}
