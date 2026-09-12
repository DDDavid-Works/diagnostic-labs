CREATE TABLE [dbo].[ClinicalChemistries2]
(
	[Id] BIGINT NOT NULL IDENTITY,
    [PatientId] BIGINT NULL,
	[PatientRegistrationId] BIGINT NULL,
    [PatientCode] NVARCHAR(200) NOT NULL,
    [PatientName] NVARCHAR(200) NOT NULL,
    [CompanyOrPhysician] NVARCHAR(200) NULL,
    [Age] NVARCHAR(50) NULL,
    [Sex] NVARCHAR(20) NULL,
    [DateRequested] DATETIME NULL,
    [Photo] VARBINARY(MAX) NULL,
    [AlkalinePhosphataseCNValue] NVARCHAR(100) NULL,
    [AlkalinePhosphataseCUnit] NVARCHAR(100) NULL,
    [AlkalinePhosphataseCResults] NVARCHAR(100) NULL,
    [AlkalinePhosphataseSNValue] NVARCHAR(100) NULL,
    [AlkalinePhosphataseSUnit] NVARCHAR(100) NULL,
    [AlkalinePhosphataseSResults] NVARCHAR(100) NULL,
    [SGOTCNValue] NVARCHAR(100) NULL,
    [SGOTCUnit] NVARCHAR(100) NULL,
    [SGOTCResults] NVARCHAR(100) NULL,
    [SGOTSNValue] NVARCHAR(100) NULL,
    [SGOTSUnit] NVARCHAR(100) NULL,
    [SGOTSResults] NVARCHAR(100) NULL,
    [MedicalTechnologist] NVARCHAR(100) NOT NULL,
    [Pathologist] NVARCHAR(100) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [ClinicalChemistry2_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
