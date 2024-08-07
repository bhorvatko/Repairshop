﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.Data.Configurations;

internal class WarrantTemplateStepConfiguration
    : IEntityTypeConfiguration<WarrantTemplateStep>
{
    public void Configure(EntityTypeBuilder<WarrantTemplateStep> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.WarrantTemplateSteps);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .HasOne(x => x.Procedure)
            .WithMany(x => x.WarrantTemplateSteps)
            .HasForeignKey(x => x.ProcedureId);

        builder
            .HasOne(x => x.WarrantTemplate)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.WarrantTemplateId);
    }
}
