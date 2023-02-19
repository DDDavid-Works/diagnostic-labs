CREATE VIEW [dbo].[PaymentDetails]
AS
SELECT py.Id AS 'PaymentId', py.PaymentDate, pr.Id AS 'PatientRegistrationId', pr.InputDate AS 'PatientRegistrationDate',
pt.Id AS 'PatientId', pt.PatientName, c.Id AS 'CompanyId', c.CompanyName
FROM Payments py 
INNER JOIN PatientRegistrations pr ON pr.Id = py.PatientRegistrationId
INNER JOIN Patients pt ON pt.Id = pr.PatientId
LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId
WHERE py.IsActive = 1
