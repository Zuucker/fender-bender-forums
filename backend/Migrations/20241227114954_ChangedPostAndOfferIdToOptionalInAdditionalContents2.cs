using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPostAndOfferIdToOptionalInAdditionalContents2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalContents_Offers_OfferId",
                table: "AdditionalContents");

            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalContents_Posts_PostId",
                table: "AdditionalContents");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "AdditionalContents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "AdditionalContents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalContents_Offers_OfferId",
                table: "AdditionalContents",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalContents_Posts_PostId",
                table: "AdditionalContents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "AdditionalContents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "AdditionalContents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
    }
}
