CREATE VIEW [dbo].[PatientCompanies]
AS 
SELECT DISTINCT p.Id AS 'PatientId', p.PatientCode, p.PatientName, ISNULL(c.Id, NULL) AS 'CompanyId', ISNULL(c.CompanyName, '') AS 'CompanyName'
FROM Patients p 
LEFT OUTER JOIN PatientRegistrations pr ON pr.PatientId = p.Id
LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId
WHERE p.IsActive = 1 AND (pr.Id IS NULL OR pr.IsActive = 1)
