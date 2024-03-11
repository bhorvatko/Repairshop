using Microsoft.Extensions.DependencyInjection;

namespace Repairshop.Client.Infrastructure.MessageDialog;
internal static class Setup
{
    public static IServiceCollection AddMessageDialog(this IServiceCollection services) =>
        services
            .AddTransient<MessageDialogService>();
}
