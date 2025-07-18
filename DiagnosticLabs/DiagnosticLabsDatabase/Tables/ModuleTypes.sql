﻿CREATE TABLE [dbo].[ModuleTypes]
(
	[Id] INT NOT NULL IDENTITY,
    [ModuleTypeName] NVARCHAR(50) NOT NULL,
    [Icon] NVARCHAR(50) NOT NULL DEFAULT '',
	[SortOrder] INT NOT NULL DEFAULT 0, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsAdmin] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [ModuleType_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
