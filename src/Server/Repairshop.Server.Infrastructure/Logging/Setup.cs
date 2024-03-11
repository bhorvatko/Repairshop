using Microsoft.Extensions.Hosting;
using Serilog;

namespace Repairshop.Server.Infrastructure.Logging;
public static class Setup
{
    public static IHostBuilder UseLogging(this IHostBuilder host) =>
        host.UseSerilog((context, config) =>
        {
            config
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Console();
        });
}
