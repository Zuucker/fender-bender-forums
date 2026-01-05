using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ReaddedTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<TagDto>>(
                name: "Tags",
                table: "Posts",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<List<TagDto>>(
                name: "Tags",
                table: "Offers",
                type: "jsonb",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Offers");
        }
    }
}
