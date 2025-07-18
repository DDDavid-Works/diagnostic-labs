﻿CREATE TABLE [dbo].[DiscountDetails]
(
	[Id] BIGINT NOT NULL IDENTITY,
	[DiscountId] BIGINT NOT NULL DEFAULT 0,
	[Amount] DECIMAL(18,4) NULL DEFAULT NULL,
	[Percentage] DECIMAL(18,4) NULL DEFAULT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
	[CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [DiscountDetail_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_DiscountDetail_Discounts] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts]([Id])
)