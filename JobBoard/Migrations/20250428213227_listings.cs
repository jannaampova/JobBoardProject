using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class listings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedListings",
                columns: table => new
                {
                    candidateId = table.Column<int>(type: "int", nullable: false),
                    listingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedListings", x => new { x.candidateId, x.listingId });

                    table.ForeignKey(
                        name: "FK_SavedListings_Candidate_candidateId",
                        column: x => x.candidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Candidate can be Cascade

                    table.ForeignKey(
                        name: "FK_SavedListings_Listings_listingId",
                        column: x => x.listingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Important: Restrict to avoid cascade conflict
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedListings_listingId",
                table: "SavedListings",
                column: "listingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedListings");
        }
    }
}
