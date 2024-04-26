using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Forms;
using System.Reflection;

namespace Repairshop.Client.Infrastructure.Forms;

internal static class Setup
{
    public static IServiceCollection AddForms(this IServiceCollection services)
    {
        services
            .AddTransient<IFormService, FormService>();

        services.Scan(scan =>
            scan
                .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.AssignableTo<IFormViewModel>())
                .AddClasses(classes => classes.AssignableTo<FormBase>())
                .AsSelf()
                .WithTransientLifetime());

        return services;
    }
}
