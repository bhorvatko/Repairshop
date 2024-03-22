using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Infrastructure.MessageDialog;
internal static class Setup
{
    public static IServiceCollection AddMessageDialog(this IServiceCollection services) =>
        services
            .AddTransient<IMessageDialogService, MessageDialogService>();
}
