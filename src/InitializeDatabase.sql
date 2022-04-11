CREATE SCHEMA orders AUTHORIZATION dbo
GO

CREATE TABLE orders.Customers
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Email] VARCHAR(255) NOT NULL,
	[Name] VARCHAR(200) NULL,
    [WelcomeEmailWasSent] BIT NOT NULL,
	CONSTRAINT [PK_orders_Customers_Id] PRIMARY KEY ([Id] ASC)
)
GO

INSERT INTO orders.Customers VALUES ('C75399FC-CD06-4F61-93E8-0D2147B00557', 'aykut@aykutaktas.net', 'AYKUT AKTAS', 1);

CREATE TABLE orders.Orders
(
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[IsRemoved] [bit] NOT NULL,
	[Description] [varchar](200) NULL,
	[StatusId] [tinyint] NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[OrderChangeDate] [datetime2](7) NULL,
	CONSTRAINT [PK_orders_Orders_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE orders.Plannings
(
	[Id] [uniqueidentifier] NOT NULL,
	[Source] [varchar](200) NULL,
	[Target] [varchar](200) NULL,
	CONSTRAINT [PK_orders_Plannings_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE orders.OrderPlannings
(
	[OrderId] [uniqueidentifier] NOT NULL,
	[PlanningId] [uniqueidentifier] NOT NULL,
	[SeatCapacity] [int] NULL,
	[FreeCapacity] [int] NULL,
	CONSTRAINT [PK_orders_OrderPlannings_OrderId_PlanningId] PRIMARY KEY ([OrderId] ASC, [PlanningId] ASC)
)
GO

CREATE SCHEMA app AUTHORIZATION dbo
GO

CREATE TABLE app.OutboxMessages
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_app_OutboxMessages_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE app.InternalCommands
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EnqueueDate] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_app_InternalCommands_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE VIEW orders.v_Customers
AS
(
	SELECT
		[Customer].[Id],
		[Customer].[Email],
		[Customer].[Name]
	FROM [orders].[Customers] AS [Customer]
)
GO

CREATE VIEW orders.v_OrderPlannings
AS
(
	SELECT
		[OrderPlanning].[OrderId],
		[OrderPlanning].[PlanningId],
		[OrderPlanning].[SeatCapacity],
		[Planning].[Target],
		[Planning].[Source]
	FROM [orders].[OrderPlannings] AS [OrderPlanning]
		INNER JOIN [orders].[Plannings] AS [Planning]
			ON [OrderPlanning].PlanningId = [Planning].[Id]
)
GO

CREATE VIEW orders.v_Orders
AS
(
SELECT
	[Order].[Id],
	[Order].[CustomerId],
	[Order].[IsRemoved],
    [Order].[Description],
	[Order].[OrderDate]
FROM [orders].[Orders] AS [Order]
)
GO

CREATE VIEW orders.v_Plannings
AS
(
	SELECT  [Planning].Id, 
		[OrderPlanning].SeatCapacity, 
		[Planning].Target, 
		[Planning].Source
	FROM  [orders].[OrderPlannings] AS [OrderPlanning] 
	INNER JOIN [orders].[Plannings] AS [Planning] 
		ON [OrderPlanning].PlanningId = [Planning].[Id]
)