﻿CREATE TABLE [dbo].[Payments]
(
    [Id] BIGINT NOT NULL IDENTITY,
    [PaymentDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [PatientRegistrationId] BIGINT NOT NULL DEFAULT 0,
    [PaymentAmount] DECIMAL(18, 4) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedByUserId] BIGINT NOT NULL DEFAULT 0,
    [UpdatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PaymentId_Id] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_Payment_PatientRegistration] FOREIGN KEY ([PatientRegistrationId]) REFERENCES [PatientRegistrations]([Id])
)
