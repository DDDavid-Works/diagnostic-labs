CREATE VIEW [dbo].[PatientRegistrationBatches]
AS
SELECT DISTINCT CompanyId, BatchName FROM PatientRegistrations
