using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement;

public static class Setup
{
    public static IServiceCollection AddWarrantManagement(this IServiceCollection services) =>
        services
            .AddTransient<WarrantPreviewControlViewModelFactory>()
            .AddTransient<TechnicianDashboardViewModelFactory>()
            .AddTransient<ProcedureLegendViewModel>();
}
