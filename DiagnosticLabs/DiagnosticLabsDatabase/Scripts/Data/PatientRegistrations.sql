SET IDENTITY_INSERT PatientRegistrations ON

IF NOT EXISTS (SELECT * FROM PatientRegistrations WHERE RegistrationCode = '000-000-000')
BEGIN
	INSERT INTO PatientRegistrations(Id, RegistrationCode, InputDate, PatientId, CompanyId, PackageId, BatchName, AmountDue, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId , UpdatedDate)
	VALUES (1, '000-000-000', '01-01-2001', 1, NULL, NULL, 'BATCH 000', 0, 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT PatientRegistrations OFF
GO
