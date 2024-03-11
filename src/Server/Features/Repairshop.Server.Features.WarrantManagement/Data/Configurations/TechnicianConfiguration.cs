using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;

internal class TechnicianConfiguration
    : IEntityTypeConfiguration<Technician>
{
    public void Configure(EntityTypeBuilder<Technician> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Technicians);

        builder
            .Property(x => x.Name)
            .HasMaxLength(100);

        builder
            .HasMany(x => x.Warrants)
            .WithOne(x => x.Technician)
            .HasForeignKey(x => x.TechnicianId);
    }
}
