using Microsoft.EntityFrameworkCore.Migrations;
using Models;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedTriggerForOfferScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "OfferRates");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Offers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OfferRates",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "OfferRates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "OfferRates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_OfferRates_UserId",
                table: "OfferRates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRates_AspNetUsers_UserId",
                table: "OfferRates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Trigger for INSERT
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION update_offer_on_rating_insert()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE ""Offers""
                    SET 
                        ""Rating"" = (""Rating"" * ""RatingCount"" + NEW.""Rating"") / (""RatingCount"" + 1),
                        ""RatingCount"" = ""RatingCount"" + 1
                    WHERE ""OfferId"" = NEW.""OfferId"";
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER rating_insert_trigger
                AFTER INSERT ON ""OfferRates""
                FOR EACH ROW
                EXECUTE FUNCTION update_offer_on_rating_insert();
            ");

            // Trigger for UPDATE
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION update_offer_on_rating_update()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE ""Offers""
                    SET 
                        ""Rating"" = (""Rating"" * ""RatingCount"" - OLD.""Rating"" + NEW.""Rating"") / ""RatingCount""
                    WHERE ""OfferId"" = NEW.""OfferId"";
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER rating_update_trigger
                AFTER UPDATE ON ""OfferRates""
                FOR EACH ROW
                EXECUTE FUNCTION update_offer_on_rating_update();
            ");

            // Trigger for DELETE
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION update_offer_on_rating_delete()
                RETURNS TRIGGER AS $$
                BEGIN
                    UPDATE ""Offers""
                    SET 
                        ""Rating"" = CASE 
                                    WHEN ""RatingCount"" > 1 THEN (""Rating"" * ""RatingCount"" - OLD.""Rating"") / (""RatingCount"" - 1)
                                    ELSE 0 
                                END,
                        ""RatingCount"" = ""RatingCount"" - 1
                    WHERE ""OfferId"" = OLD.""OfferId"";
                    RETURN OLD;
                END;
                $$ LANGUAGE plpgsql;

                CREATE TRIGGER rating_delete_trigger
                AFTER DELETE ON ""OfferRates""
                FOR EACH ROW
                EXECUTE FUNCTION update_offer_on_rating_delete();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS rating_insert_trigger ON ""OfferRates"";
                DROP TRIGGER IF EXISTS rating_update_trigger ON ""OfferRates"";
                DROP TRIGGER IF EXISTS rating_delete_trigger ON ""OfferRates"";

                DROP FUNCTION IF EXISTS update_offer_on_rating_insert();
                DROP FUNCTION IF EXISTS update_offer_on_rating_update();
                DROP FUNCTION IF EXISTS update_offer_on_rating_delete();
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRates_AspNetUsers_UserId",
                table: "OfferRates");

            migrationBuilder.DropIndex(
                name: "IX_OfferRates_UserId",
                table: "OfferRates");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "OfferRates");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "OfferRates");

            migrationBuilder.Sql("TRUNCATE TABLE \"OfferRates\";");

            //work around

            migrationBuilder.AddColumn<int>(
                name: "UserIdTemp",
                table: "OfferRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                    UPDATE ""OfferRates""
                    SET ""UserIdTemp"" = CAST(""UserId"" AS INTEGER);
                ");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OfferRates");

            migrationBuilder.RenameColumn(
                name: "UserIdTemp",
                table: "OfferRates",
                newName: "UserId");
            //

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "OfferRates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
