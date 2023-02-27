using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eateries.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Orderdishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDishes_Dishes_DishesId",
                table: "OrderDishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDishes",
                table: "OrderDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrderDishes_OrderId",
                table: "OrderDishes");

            migrationBuilder.RenameColumn(
                name: "DishesId",
                table: "OrderDishes",
                newName: "DishId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDishes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDishes",
                table: "OrderDishes",
                columns: new[] { "OrderId", "DishId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDishes_DishId",
                table: "OrderDishes",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDishes_Dishes_DishId",
                table: "OrderDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDishes_Dishes_DishId",
                table: "OrderDishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDishes",
                table: "OrderDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrderDishes_DishId",
                table: "OrderDishes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDishes");

            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "OrderDishes",
                newName: "DishesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDishes",
                table: "OrderDishes",
                columns: new[] { "DishesId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDishes_OrderId",
                table: "OrderDishes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDishes_Dishes_DishesId",
                table: "OrderDishes",
                column: "DishesId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
