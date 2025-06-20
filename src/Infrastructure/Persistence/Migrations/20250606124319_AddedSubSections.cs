using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentSectionId",
                table: "Sections",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ParentSectionId",
                table: "Sections",
                column: "ParentSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentSectionId",
                table: "Sections",
                column: "ParentSectionId",
                principalTable: "Sections",
                principalColumn: "SectionId");

            migrationBuilder.Sql("""
                delete from "Sections";
                """);

            migrationBuilder.Sql("""
                INSERT INTO "Sections" ("Name", "Description", "ParentSectionId")
                VALUES ('Show Room', 'Show off your car!', NULL);


                INSERT INTO "Sections" ("Name", "Description", "ParentSectionId")
                VALUES 
                ('Sports Cars', 'High-performance, speed-focused vehicles.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Muscle Cars', 'American-made, V8-powered classics like the Mustang or Camaro.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Old School / Classics', 'Vintage cars, typically pre-1980s.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('JDM', 'Japanese cars like Nissan, Toyota, Subaru, and Honda.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Euro Cars', 'European vehicles like BMW, Audi, Mercedes, and VW.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Luxury & Exotic', 'Premium high-end cars such as Ferraris, Lamborghinis, and Bentleys.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Custom Builds / Modified', 'Vehicles with significant visual or performance modifications.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Trucks & Off-Road', 'Pickups, SUVs, and vehicles built for off-road adventures.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Drift Cars', 'Custom made for taking corners sideways', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room')),
                ('Daily Drivers', 'Reliable, practical cars used for everyday transportation.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Show Room'));



                INSERT INTO "Sections" ("Name", "Description", "ParentSectionId")
                VALUES ('Stories', 'Share storier about car related topics!', NULL);


                INSERT INTO "Sections" ("Name", "Description", "ParentSectionId")
                VALUES 
                ('How I Got It', 'Stories about how users found or acquired their cars.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Stories')),
                ('Road Trip Adventures', 'Memorable road trips and the stories they created.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Stories')),
                ('Build Journey', 'Ongoing modification or restoration stories.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Stories')),
                ('Lessons Learned', 'Ownership experiences and the lessons they taught.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Stories'));



                INSERT INTO "Sections" ("Name", "Description", "ParentSectionId")
                VALUES ('Do it Yourself', 'Handy guides how to fix stuff around your car!', NULL);


                INSERT INTO "Sections" ("Name", "Description", "ParentSectionId")
                VALUES 
                ('Fixing Common Issues', 'DIY guides for common car problems and solutions.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Do it Yourself')),
                ('Upgrades & Mods', 'Step-by-step instructions for modifying or upgrading your car.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Do it Yourself')),
                ('Routine Maintenance', 'Basic maintenance like oil changes, tire rotations, and more.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Do it Yourself')),
                ('Legal & Paperwork', 'How to handle legal steps like registration or importing.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Do it Yourself')),
                ('Wiring & Electronics', 'How to install car electronics, lighting, and audio.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Do it Yourself')),
                ('Beginner Tutorials', 'Simple projects for those new to working on cars.', (SELECT "SectionId" FROM "Sections" WHERE "Name" = 'Do it Yourself'));
                
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                delete from "Sections";
                """);

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentSectionId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ParentSectionId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ParentSectionId",
                table: "Sections");
        }
    }
}
