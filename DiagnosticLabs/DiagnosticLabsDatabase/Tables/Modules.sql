﻿CREATE TABLE [dbo].[Modules]
(
	[Id] INT NOT NULL IDENTITY,
	[ModuleTypeId] INT NOT NULL,
    [ModuleName] NVARCHAR(50) NOT NULL,
	[HasView] BIT NOT NULL DEFAULT 0,
    [HasCreate] BIT NOT NULL DEFAULT 0,
    [HasEdit] BIT NOT NULL DEFAULT 0,
    [HasDelete] BIT NOT NULL DEFAULT 0,
    [HasSearch] BIT NOT NULL DEFAULT 0,
    [HasPrint] BIT NOT NULL DEFAULT 0, 
    [HasShowList] BIT NOT NULL DEFAULT 0, 
    [HasShowSetDefaults] BIT NOT NULL DEFAULT 0, 
    [Icon] NVARCHAR(50) NOT NULL DEFAULT '', 
    [SortOrder] INT NOT NULL DEFAULT 0, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [ServiceId] BIGINT NULL, 
    CONSTRAINT [Module_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_Modules_ModuleTypes] FOREIGN KEY ([ModuleTypeId]) REFERENCES [ModuleTypes]([Id]),
    CONSTRAINT [FK_Modules_Services] FOREIGN KEY ([ServiceId]) REFERENCES [Services]([Id])
)