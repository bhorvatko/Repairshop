using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Data.Converters;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Data;

public class WarrantManagementDbContext
    : DbContext
{
    public WarrantManagementDbContext(DbContextOptions<WarrantManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("wm");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarrantManagementDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder
            .Properties<ColorCode>()
            .HaveConversion<ColorCodeConverter>();

        builder
            .Properties<ProcedurePriority>()
            .HaveConversion<ProcedurePriorityConverter>();  
    }
}
