using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OfferFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CityId",
                table: "Offers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRates_OfferId",
                table: "OfferRates",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRates_Offers_OfferId",
                table: "OfferRates",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Cities_CityId",
                table: "Offers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferRates_Offers_OfferId",
                table: "OfferRates");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Cities_CityId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CityId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_OfferRates_OfferId",
                table: "OfferRates");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Offers");
        }
    }
}
