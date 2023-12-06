using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISD_Project.Server.Migrations
{
    /// <inheritdoc />
    public partial class Release012updateHealthInfomationInsuranceInsuranceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Users_UserID",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_UserID",
                table: "Insurances");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "BMI",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "CurrentMedications",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "LifestyleHabits",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "MedicalHistory",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "VaccinationHistory",
                table: "HealthInformation");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Insurances",
                newName: "CoveragePeriodInYears");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceAmount",
                table: "Insurances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "DetailDescription",
                table: "Insurances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceTypeId",
                table: "Insurances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryDescription",
                table: "Insurances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "AlcoholConsumption",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CigarettesPerDay",
                table: "HealthInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DangerousSportsDetails",
                table: "HealthInformation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DaysPerWeekAlcohol",
                table: "HealthInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DiagnosedWithHealthConditions",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DrugUse",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EngagesInDangerousSports",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ExperiencedDiseasesDetails",
                table: "HealthInformation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ExperiencedDiseasesInLast5Years",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSpecificHealthConditions",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OtherDisabilities",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherDisabilitiesDetails",
                table: "HealthInformation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Smoking",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UnexplainedWeightLoss",
                table: "HealthInformation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UnexplainedWeightLossDetails",
                table: "HealthInformation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InsuranceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceTypeId",
                table: "Insurances",
                column: "InsuranceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_InsuranceTypes_InsuranceTypeId",
                table: "Insurances",
                column: "InsuranceTypeId",
                principalTable: "InsuranceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_InsuranceTypes_InsuranceTypeId",
                table: "Insurances");

            migrationBuilder.DropTable(
                name: "InsuranceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_InsuranceTypeId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DetailDescription",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "InsuranceTypeId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "SummaryDescription",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "AlcoholConsumption",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "CigarettesPerDay",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "DangerousSportsDetails",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "DaysPerWeekAlcohol",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "DiagnosedWithHealthConditions",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "DrugUse",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "EngagesInDangerousSports",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "ExperiencedDiseasesDetails",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "ExperiencedDiseasesInLast5Years",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "HasSpecificHealthConditions",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "OtherDisabilities",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "OtherDisabilitiesDetails",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "Smoking",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "UnexplainedWeightLoss",
                table: "HealthInformation");

            migrationBuilder.DropColumn(
                name: "UnexplainedWeightLossDetails",
                table: "HealthInformation");

            migrationBuilder.RenameColumn(
                name: "CoveragePeriodInYears",
                table: "Insurances",
                newName: "UserID");

            migrationBuilder.AlterColumn<float>(
                name: "PriceAmount",
                table: "Insurances",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Insurances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Insurances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "BMI",
                table: "HealthInformation",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<List<string>>(
                name: "CurrentMedications",
                table: "HealthInformation",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "LifestyleHabits",
                table: "HealthInformation",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "MedicalHistory",
                table: "HealthInformation",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "VaccinationHistory",
                table: "HealthInformation",
                type: "text[]",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Insurance" });

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_UserID",
                table: "Insurances",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Users_UserID",
                table: "Insurances",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
