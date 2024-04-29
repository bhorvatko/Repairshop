using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;
internal class WarrantStepConfiguration
    : IEntityTypeConfiguration<WarrantStep>
{
    public void Configure(EntityTypeBuilder<WarrantStep> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.WarrantSteps);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .HasOne(x => x.Procedure)
            .WithMany(x => x.WarrantSteps)
            .HasForeignKey(x => x.ProcedureId);

        builder
            .HasOne(x => x.NextTransition)
            .WithOne(x => x.PreviousStep)
            .HasForeignKey<WarrantStepTransition>(x => x.PreviousStepId);

        builder
            .HasOne(x => x.PreviousTransition)
            .WithOne(x => x.NextStep)
            .HasForeignKey<WarrantStepTransition>(x => x.NextStepId);
    }
}
