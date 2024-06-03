using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;
internal class WarrantConfiguration
    : IEntityTypeConfiguration<Warrant>
{
    public void Configure(EntityTypeBuilder<Warrant> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Warrants);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.Title)
            .HasMaxLength(100);

        builder
            .HasMany(x => x.Steps)
            .WithOne(x => x.Warrant)
            .HasForeignKey(x => x.WarrantId);

        builder
            .HasOne(x => x.CurrentStep)
            .WithOne()
            .HasForeignKey<Warrant>(x => x.CurrentStepId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(x => x.LogEntries)
            .WithOne();
    }
}
