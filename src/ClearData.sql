DELETE FROM orders.Plannings
GO

DELETE FROM orders.OrderPlannings
GO

DELETE FROM orders.Orders
GO

DELETE FROM orders.Customers
WHERE Id NOT IN ('C75399FC-CD06-4F61-93E8-0D2147B00557')

DELETE FROM app.OutboxMessages
GO

DELETE FROM app.InternalCommands
GO