﻿CREATE TABLE [dbo].[PatientRegistrations]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [RegistrationCode] NVARCHAR(100) NOT NULL DEFAULT '',
    [InputDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[PatientId] BIGINT NOT NULL,
	[CompanyId] BIGINT NULL DEFAULT 0,
	[PackageId] BIGINT NULL DEFAULT 0,
    [BatchName] NVARCHAR(200) NOT NULL,
    [AmountDue] DECIMAL(18, 4) NOT NULL,
    [DiscountAmount] DECIMAL(18, 4) NULL DEFAULT 0,
    [DiscountPercentage] DECIMAL(18, 4) NULL DEFAULT NULL,
    [DiscountTotal] DECIMAL(18, 4) NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PatientRegistration_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_PatientRegistration_Patients] FOREIGN KEY ([PatientId]) REFERENCES [Patients]([Id]),
    CONSTRAINT [FK_PatientRegistration_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [Companies]([Id]),
    CONSTRAINT [FK_PatientRegistration_Packages] FOREIGN KEY ([PackageId]) REFERENCES [Packages]([Id])
)
