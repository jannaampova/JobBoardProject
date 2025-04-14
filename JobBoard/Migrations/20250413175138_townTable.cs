using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class townTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "townId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Town",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_townId",
                table: "Listings",
                column: "townId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Town_townId",
                table: "Listings",
                column: "townId",
                principalTable: "Town",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Town_townId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Town");

            migrationBuilder.DropIndex(
                name: "IX_Listings_townId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "townId",
                table: "Listings");
        }
    }
}
