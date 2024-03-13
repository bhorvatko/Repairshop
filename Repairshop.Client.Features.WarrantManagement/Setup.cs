using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Technicians;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement;

public static class Setup
{
    public static IServiceCollection AddWarrantManagement(this IServiceCollection services) =>
        services
            .AddTransient<ProceduresViewModel>()
            .AddTransient<ProceduresView>()
            .AddTransient<CreateWarrantViewModel>()
            .AddTransient<CreateWarrantView>()
            .AddTransient<DashboardViewModel>()
            .AddTransient<TechnicianDashboardViewModel>()
            .AddTransient<WarrantPreviewControlViewModel>()
            .AddTransient<AddTechnicianViewModel>()
            .AddTransient<EditWarrantViewModel>()
            .AddTransient<CreateWarrantViewModel>();
}
