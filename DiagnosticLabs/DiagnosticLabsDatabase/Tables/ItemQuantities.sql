﻿CREATE TABLE [dbo].[ItemQuantities]
(
	[Id] BIGINT NOT NULL IDENTITY,
	[ItemId] BIGINT NOT NULL DEFAULT 0,
	[ItemLocationId] BIGINT NOT NULL DEFAULT 0,
	[Quantity] DECIMAL(18,4) NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1, 
	[CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [ItemQuantity_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_ItemQuantity_Items] FOREIGN KEY ([ItemId]) REFERENCES [Items]([Id]),
    CONSTRAINT [FK_ItemQuantity_ItemLocations] FOREIGN KEY ([ItemLocationId]) REFERENCES [ItemLocations]([Id])
)
