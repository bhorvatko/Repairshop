using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;
using Repairshop.Client.Features.WarrantManagement.Technicians;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Client.Features.WarrantManagement;

public static class Setup
{
    public static IServiceCollection AddWarrantManagement(this IServiceCollection services) =>
        services
            .AddTransient<WarrantPreviewControlViewModelFactory>()
            .AddTransient<TechnicianDashboardViewModelFactory>()
            .AddTransient<ProcedureLegendViewModel>()
            .AddTransient<WarrantFilterSelectionViewModelFactory>()
            .AddTransient<EditWarrantViewModel>()
            .AddTransient<EditTechnicianViewModel>()
            .AddTransient<EditWarrantTemplateViewModel>();
}
