﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repairshop.Server.Features.WarrantManagement.Data;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations
{
    [DbContext(typeof(WarrantManagementDbContext))]
    [Migration("20240226094623_AddWarrantsAndTechnicians")]
    partial class AddWarrantsAndTechnicians
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("wm")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Procedures.Procedure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Procedures", "wm");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Technicians.Technician", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Technicians", "wm");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.Warrant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentStepId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUrgent")
                        .HasColumnType("bit");

                    b.Property<Guid?>("TechnicianId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentStepId")
                        .IsUnique()
                        .HasFilter("[CurrentStepId] IS NOT NULL");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Warrants", "wm");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProcedureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WarrantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProcedureId");

                    b.HasIndex("WarrantId");

                    b.ToTable("WarrantSteps", "wm");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStepTransition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CanBePerformedByFrontOffice")
                        .HasColumnType("bit");

                    b.Property<bool>("CanBePerformedByWorkshop")
                        .HasColumnType("bit");

                    b.Property<Guid>("NextStepId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PreviousStepId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NextStepId")
                        .IsUnique();

                    b.HasIndex("PreviousStepId")
                        .IsUnique();

                    b.ToTable("WarrantStepTransitions", "wm");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.Warrant", b =>
                {
                    b.HasOne("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep", "CurrentStep")
                        .WithOne()
                        .HasForeignKey("Repairshop.Server.Features.WarrantManagement.Warrants.Warrant", "CurrentStepId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Repairshop.Server.Features.WarrantManagement.Technicians.Technician", "Technician")
                        .WithMany("Warrants")
                        .HasForeignKey("TechnicianId");

                    b.Navigation("CurrentStep");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep", b =>
                {
                    b.HasOne("Repairshop.Server.Features.WarrantManagement.Procedures.Procedure", "Procedure")
                        .WithMany()
                        .HasForeignKey("ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repairshop.Server.Features.WarrantManagement.Warrants.Warrant", null)
                        .WithMany("Steps")
                        .HasForeignKey("WarrantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Procedure");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStepTransition", b =>
                {
                    b.HasOne("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep", "NextStep")
                        .WithOne("PreviousTransition")
                        .HasForeignKey("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStepTransition", "NextStepId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep", "PreviousStep")
                        .WithOne("NextTransition")
                        .HasForeignKey("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStepTransition", "PreviousStepId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("NextStep");

                    b.Navigation("PreviousStep");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Technicians.Technician", b =>
                {
                    b.Navigation("Warrants");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.Warrant", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep", b =>
                {
                    b.Navigation("NextTransition");

                    b.Navigation("PreviousTransition");
                });
#pragma warning restore 612, 618
        }
    }
}
