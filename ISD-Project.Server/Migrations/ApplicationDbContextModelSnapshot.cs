﻿// <auto-generated />
using System;
using ISD_Project.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISD_Project.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ISD_Project.Server.InsuranceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("InsuranceTypes");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.HealthInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("AlcoholConsumption")
                        .HasColumnType("boolean");

                    b.Property<int>("CigarettesPerDay")
                        .HasColumnType("integer");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("DangerousSportsDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DaysPerWeekAlcohol")
                        .HasColumnType("integer");

                    b.Property<bool>("DiagnosedWithHealthConditions")
                        .HasColumnType("boolean");

                    b.Property<bool>("DrugUse")
                        .HasColumnType("boolean");

                    b.Property<bool>("EngagesInDangerousSports")
                        .HasColumnType("boolean");

                    b.Property<string>("ExperiencedDiseasesDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ExperiencedDiseasesInLast5Years")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasSpecificHealthConditions")
                        .HasColumnType("boolean");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Smoking")
                        .HasColumnType("boolean");

                    b.Property<bool>("UnexplainedWeightLoss")
                        .HasColumnType("boolean");

                    b.Property<string>("UnexplainedWeightLossDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("HealthInformation");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Insurance", b =>
                {
                    b.Property<int>("InsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InsuranceId"));

                    b.Property<int>("CoveragePeriodInYears")
                        .HasColumnType("integer");

                    b.Property<string>("DetailDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InsuranceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("InsuranceTypeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("PriceAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("SummaryDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("InsuranceId");

                    b.HasIndex("InsuranceTypeId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "FinancialDepartment"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ValidationDepartment"
                        },
                        new
                        {
                            Id = 5,
                            Name = "CustomerCareDepartment"
                        });
                });

            modelBuilder.Entity("ISD_Project.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateIssued")
                        .HasColumnType("date");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("IdentityDocumentId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("ValidUntil")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ISD_Project.Server.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IsActivated")
                        .HasColumnType("integer");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime?>("RestTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("VerificationToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Admin", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Customer", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.Property<int?>("HealthInformationId")
                        .HasColumnType("integer");

                    b.Property<int>("IsApproved")
                        .HasColumnType("integer");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.CustomerCareDepartment", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.HasDiscriminator().HasValue("CustomerCareDepartment");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.FinancialDepartment", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.HasDiscriminator().HasValue("FinancialDepartment");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.ValidationDepartment", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.HasDiscriminator().HasValue("ValidationDepartment");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.HealthInformation", b =>
                {
                    b.HasOne("ISD_Project.Server.Models.Customer", "Customer")
                        .WithOne("HealthInformation")
                        .HasForeignKey("ISD_Project.Server.Models.HealthInformation", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Insurance", b =>
                {
                    b.HasOne("ISD_Project.Server.InsuranceType", "InsuranceType")
                        .WithMany("Insurances")
                        .HasForeignKey("InsuranceTypeId");

                    b.Navigation("InsuranceType");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.UserAccount", b =>
                {
                    b.HasOne("ISD_Project.Server.Models.User", "User")
                        .WithOne("UserAccount")
                        .HasForeignKey("ISD_Project.Server.Models.UserAccount", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.UserRole", b =>
                {
                    b.HasOne("ISD_Project.Server.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ISD_Project.Server.Models.UserAccount", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ISD_Project.Server.InsuranceType", b =>
                {
                    b.Navigation("Insurances");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.User", b =>
                {
                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.UserAccount", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Customer", b =>
                {
                    b.Navigation("HealthInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
