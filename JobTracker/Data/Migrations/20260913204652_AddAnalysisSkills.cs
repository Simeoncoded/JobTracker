using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnalysisSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "MatchingSkills",
                table: "JobAnalyses",
                type: "text[]",
                nullable: false,
                defaultValue: Array.Empty<string>());

            migrationBuilder.AddColumn<List<string>>(
                name: "MissingSkills",
                table: "JobAnalyses",
                type: "text[]",
                nullable: false,
                defaultValue: Array.Empty<string>());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchingSkills",
                table: "JobAnalyses");

            migrationBuilder.DropColumn(
                name: "MissingSkills",
                table: "JobAnalyses");
        }
    }
}
