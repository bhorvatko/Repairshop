using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;
internal class WarrantTemplateConfiguration
    : IEntityTypeConfiguration<WarrantTemplate>
{
    public void Configure(EntityTypeBuilder<WarrantTemplate> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.WarrantTemplates);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .HasMany(x => x.Steps)
            .WithOne()
            .HasForeignKey(x => x.WarrantTemplateId);
    }
}