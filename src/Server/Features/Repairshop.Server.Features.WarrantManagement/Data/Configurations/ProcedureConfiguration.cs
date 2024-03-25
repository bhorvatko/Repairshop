using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;
internal class ProcedureConfiguration
    : IEntityTypeConfiguration<Procedure>
{
    public void Configure(EntityTypeBuilder<Procedure> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Procedures);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Color)
            .HasMaxLength(6);
    }
}
