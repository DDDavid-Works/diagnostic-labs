CREATE TABLE [dbo].[PatientRegistrationServices]
(
	[Id] BIGINT NOT NULL IDENTITY,
	[PatientRegistrationId] BIGINT NOT NULL DEFAULT 0,
	[ServiceId] BIGINT NOT NULL DEFAULT 0,
	[Price] DECIMAL(18,4) NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1, 
	[CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PatientRegistrationService_Id] PRIMARY KEY CLUSTERED 
    (
	    [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_PatientRegistrationService_Patients] FOREIGN KEY ([PatientRegistrationId]) REFERENCES [PatientRegistrations]([Id]),
    CONSTRAINT [FK_PatientRegistrationService_Services] FOREIGN KEY ([ServiceId]) REFERENCES [Services]([Id])
)
