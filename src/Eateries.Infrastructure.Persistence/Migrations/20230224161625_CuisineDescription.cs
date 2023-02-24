using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eateries.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CuisineDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cuisines",
                keyColumn: "Id",
                keyValue: new Guid("a5b0f2b7-5e98-4a7c-836f-0c7a1b9a88b7"));

            migrationBuilder.DeleteData(
                table: "Cuisines",
                keyColumn: "Id",
                keyValue: new Guid("c58f23af-65e4-4ca1-9fb4-6b1dfc7a48a8"));

            migrationBuilder.DeleteData(
                table: "Cuisines",
                keyColumn: "Id",
                keyValue: new Guid("f43a5871-1037-4ef5-ae6d-d08e1b5c7461"));

            migrationBuilder.RenameColumn(
                name: "CuisineName",
                table: "Cuisines",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cuisines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cuisines");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cuisines",
                newName: "CuisineName");

            migrationBuilder.InsertData(
                table: "Cuisines",
                columns: new[] { "Id", "Created", "CreatedBy", "CuisineName", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("a5b0f2b7-5e98-4a7c-836f-0c7a1b9a88b7"), null, null, "European", null, null },
                    { new Guid("c58f23af-65e4-4ca1-9fb4-6b1dfc7a48a8"), null, null, "Armenian", null, null },
                    { new Guid("f43a5871-1037-4ef5-ae6d-d08e1b5c7461"), null, null, "Russian", null, null }
                });
        }
    }
}
