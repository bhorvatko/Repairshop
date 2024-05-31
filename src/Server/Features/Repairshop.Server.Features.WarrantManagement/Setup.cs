using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Data;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement;

public static class Setup
{
    public static IServiceCollection AddWarrantManagement(
        this IServiceCollection services,
        Func<string> getDbConnectionString,
        IEnumerable<Type> interceptorTypes) =>
        services
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddDbContext<WarrantManagementDbContext>((serviceProvider, builder) =>
            {
                builder
                    .UseSqlServer(getDbConnectionString())
                    .AddInterceptors(interceptorTypes.Select(t => (IInterceptor)serviceProvider.GetRequiredService(t)));
            })
            .AddTransient<WarrantTemplateStepSequenceFactory>();
}
