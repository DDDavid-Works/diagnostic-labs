CREATE VIEW [dbo].[PatientRegistrationDetails]
AS
SELECT pr.Id AS 'PatientRegistrationId', pr.InputDate, pr.AmountDue, p.Id AS 'PatientId', p.PatientName, c.Id AS 'CompanyId', c.CompanyName
FROM Patients p 
RIGHT OUTER JOIN PatientRegistrations pr ON pr.PatientId = p.Id
LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId
WHERE p.IsActive = 1
