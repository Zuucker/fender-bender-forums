using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalContentsNtoMPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_OwnerId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OwnerId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "AdditionalContentId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Offers");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AuthorId",
                table: "Offers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_AuthorId",
                table: "Offers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_AuthorId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_AuthorId",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalContentId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Offers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OwnerId",
                table: "Offers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_OwnerId",
                table: "Offers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
