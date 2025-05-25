using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class compositeKyes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ListingRequirements_ListingId",
                table: "ListingRequirements");

            migrationBuilder.DropIndex(
                name: "IX_ListingBenefits_ListingId",
                table: "ListingBenefits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datePosted",
                table: "Listings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingRequirements",
                table: "ListingRequirements",
                columns: new[] { "ListingId", "RequirementId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingBenefits",
                table: "ListingBenefits",
                columns: new[] { "ListingId", "BenefitId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingRequirements",
                table: "ListingRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingBenefits",
                table: "ListingBenefits");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "datePosted",
                table: "Listings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_ListingRequirements_ListingId",
                table: "ListingRequirements",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingBenefits_ListingId",
                table: "ListingBenefits",
                column: "ListingId");
        }
    }
}
