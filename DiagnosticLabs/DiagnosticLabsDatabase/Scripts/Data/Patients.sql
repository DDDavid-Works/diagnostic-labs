SET IDENTITY_INSERT Patients ON

IF NOT EXISTS (SELECT * FROM Patients WHERE PatientCode = 'PAT-00-000')
BEGIN
	INSERT INTO Patients(Id, PatientCode, PatientName, DateOfBirth, Age, Gender, CivilStatus, Address, ContactNumbers, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
	VALUES (1, 'PAT-00-000', 'Default Patient', '01-01-2000', '20 years old', 'Male', 'Single', 'AC - PH', '0987654321', 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Patients OFF
GO

