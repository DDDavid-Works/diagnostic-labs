CREATE VIEW PatientRegistrationPayments
AS
SELECT pr.Id AS PatientRegistrationId, ISNULL(pr.AmountDue, 0) AS AmountDue, ISNULL(pys.AmountPaid, 0) AS AmountPaid, pr.AmountDue - ISNULL(pys.AmountPaid, 0) AS Balance FROM PatientRegistrations pr 
LEFT OUTER JOIN (SELECT pr.Id, SUM(py.PaymentAmount) AS AmountPaid FROM PatientRegistrations pr
INNER JOIN Payments py ON py.PatientRegistrationId = pr.Id
WHERE py.IsActive = 1
GROUP BY pr.Id) AS pys ON pys.Id  = pr.Id