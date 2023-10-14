CREATE VIEW [dbo].[LabResults]
AS
SELECT 'Stool/Fecalysis' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, l.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', l.CompanyOrPhysician AS Company, l.DateRequested, l.IsActive FROM StoolFecalyses l
LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
UNION
SELECT 'Annual Physical Exam' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateInputted AS 'DateRequested', l.IsActive FROM APEs l
LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId
