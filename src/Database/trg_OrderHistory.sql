USE [EateriesDb]
GO
/****** Object:  Trigger [dbo].[trg_OrderHistory]    Script Date: 2/24/2023 8:07:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trg_OrderHistory]
ON [dbo].[Orders]
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