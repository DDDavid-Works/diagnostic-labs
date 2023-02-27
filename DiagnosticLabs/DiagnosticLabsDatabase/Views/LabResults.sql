CREATE VIEW [dbo].[LabResults]
AS
SELECT 'Stool/Fecalysis' AS Service, s.Id, s.PatientRegistrationId, s.PatientId, s.PatientCode, s.PatientName, pr.CompanyId AS 'CompanyId', s.CompanyOrPhysician AS Company, s.DateRequested, s.IsActive FROM StoolFecalyses s
LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = s.PatientRegistrationId
