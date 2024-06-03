using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;

internal class WarrantLogEntryConfiguration
    : IEntityTypeConfiguration<WarrantLogEntry>
{
    public void Configure(EntityTypeBuilder<WarrantLogEntry> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.WarrantLogEntries);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
