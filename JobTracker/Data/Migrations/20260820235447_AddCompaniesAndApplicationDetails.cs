using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCompaniesAndApplicationDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "JobApplications");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "JobApplications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobApplications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JobUrl",
                table: "JobApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "JobApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "JobApplications",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CompanyId",
                table: "JobApplications",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Companies_CompanyId",
                table: "JobApplications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Companies_CompanyId",
                table: "JobApplications");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_CompanyId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobUrl",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "JobApplications");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "JobApplications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
