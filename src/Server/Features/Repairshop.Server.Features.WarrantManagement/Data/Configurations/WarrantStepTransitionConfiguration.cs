using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;

internal class WarrantStepTransitionConfiguration
    : IEntityTypeConfiguration<WarrantStepTransition>
{
    public void Configure(EntityTypeBuilder<WarrantStepTransition> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.WarrantStepTransitions);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.PreviousStep)
            .WithOne(x => x.NextTransition)
            .HasForeignKey<WarrantStepTransition>(x => x.PreviousStepId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(x => x.NextStep)
            .WithOne(x => x.PreviousTransition)
            .HasForeignKey<WarrantStepTransition>(x => x.NextStepId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
