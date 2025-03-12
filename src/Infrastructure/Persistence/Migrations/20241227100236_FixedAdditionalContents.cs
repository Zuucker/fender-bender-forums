using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixedAdditionalContents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalContentId",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "AdditionalContents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "AdditionalContents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalContents_OfferId",
                table: "AdditionalContents",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalContents_PostId",
                table: "AdditionalContents",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalContents_Offers_OfferId",
                table: "AdditionalContents",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalContents_Posts_PostId",
                table: "AdditionalContents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalContents_Offers_OfferId",
                table: "AdditionalContents");

            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalContents_Posts_PostId",
                table: "AdditionalContents");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalContents_OfferId",
                table: "AdditionalContents");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalContents_PostId",
                table: "AdditionalContents");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "AdditionalContents");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "AdditionalContents");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalContentId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
