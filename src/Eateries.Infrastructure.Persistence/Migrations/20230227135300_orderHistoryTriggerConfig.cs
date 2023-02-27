using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eateries.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class orderHistoryTriggerConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE TRIGGER trg_OrderHistory
        ON dbo.Orders
        AFTER INSERT, UPDATE
        AS
        BEGIN
            SET NOCOUNT ON;
    
            DECLARE @OrderId uniqueidentifier
            SELECT @OrderId =  Id FROM inserted
    
            DECLARE @EateryId uniqueidentifier
            SELECT @EateryId = EateryId FROM inserted
    
            DECLARE @OrderDate datetime
            SELECT @OrderDate = OrderDate FROM inserted
    
            DECLARE @Status int
            SELECT @Status = Status FROM inserted
    
            DECLARE @CompletedDate datetime
            SELECT @CompletedDate = CompletedDate FROM inserted
    
            INSERT INTO dbo.OrderHistories (OrderId, EateryId, OrderDate, Status, CompletedDate)
            VALUES (@OrderId, @EateryId, @OrderDate, @Status, @CompletedDate)
        END
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER trg_OrderHistory");
        }
    }
}
