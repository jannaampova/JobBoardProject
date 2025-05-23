using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class AddSavedListings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
