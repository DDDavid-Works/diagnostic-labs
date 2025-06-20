﻿CREATE TABLE [dbo].[StoolFecalyses]
(
	[Id] BIGINT NOT NULL IDENTITY,
	[PatientId] BIGINT NULL,
	[PatientRegistrationId] BIGINT NULL,
    [PatientCode] NVARCHAR(200) NOT NULL,
    [PatientName] NVARCHAR(200) NOT NULL,
    [CompanyOrPhysician] NVARCHAR(200) NOT NULL,
    [Age] NVARCHAR(50) NULL,
    [Sex] NVARCHAR(20) NULL,
    [DateRequested] DATETIME NULL,
    [Photo] VARBINARY(MAX) NULL,
    [Color] NVARCHAR(50) NULL,
    [Consistency] NVARCHAR(50) NULL,
    [Result] NVARCHAR(500) NOT NULL,
    [Remarks] NVARCHAR(500) NOT NULL,
    [MedicalTechnologist] NVARCHAR(100) NOT NULL,
    [Pathologist] NVARCHAR(100) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [StoolFecalysis_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
