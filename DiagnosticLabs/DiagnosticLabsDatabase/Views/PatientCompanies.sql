CREATE VIEW [dbo].[PatientCompanies]
AS 
SELECT DISTINCT p.Id AS 'PatientId', p.PatientName, c.Id AS 'CompanyId', c.CompanyName
FROM Patients p 
LEFT OUTER JOIN PatientRegistrations pr ON pr.PatientId = p.Id
LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId
WHERE p.IsActive = 1 AND pr.IsActive = 1
