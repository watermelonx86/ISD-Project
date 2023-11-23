﻿// <auto-generated />
using System;
using ISD_Project.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISD_Project.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231123063615_update some attribute")]
    partial class updatesomeattribute
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.Customer", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.CustomerCareDepartment", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.Property<int>("CustomerCareDepartmentId")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("CustomerCareDepartment");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.FinancialDepartment", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.Property<int>("FinancialDepartmentId")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("FinancialDepartment");
                });

            modelBuilder.Entity("ISD_Project.Server.Models.ValidationDepartment", b =>
                {
                    b.HasBaseType("ISD_Project.Server.Models.User");

                    b.Property<int>("ValidationDepartmentId")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("ValidationDepartment");
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
#pragma warning restore 612, 618
        }
    }
}
