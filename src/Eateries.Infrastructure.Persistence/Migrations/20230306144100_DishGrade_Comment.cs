using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eateries.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DishGradeComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "DishGrade",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "DishGrade");
        }
    }
}
