SET IDENTITY_INSERT StoolFecalyses ON

IF NOT EXISTS (SELECT * FROM StoolFecalyses WHERE PatientId = 1)
BEGIN
	INSERT INTO StoolFecalyses(Id, PatientId, PatientRegistrationId, PatientCode, PatientName, CompanyOrPhysician, Age, Sex, DateRequested, Photo, Color, Consistency, Result, Remarks, MedicalTechnologist, Pathologist, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 1, 1, 'PAT-00-000', 'Default Patient', '', '20 years old', 'Male', '01-01-2026', NULL, '', '', '', '', '', '', 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT StoolFecalyses OFF
GO

