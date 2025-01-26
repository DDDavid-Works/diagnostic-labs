CREATE VIEW [dbo].[LatestCodeNumbers]
AS
WITH CompanyCodes (Code)
AS (SELECT TOP 1 Code FROM CompanySetups ORDER BY UpdatedDate DESC)

SELECT T1.v2 AS Prefix, MAX(CAST(value AS INT)) AS MaxNumber  
FROM PatientRegistrations p CROSS APPLY string_split(RegistrationCode, '-')  
INNER JOIN (SELECT RegistrationCode, value AS v2  
FROM PatientRegistrations CROSS APPLY string_split(RegistrationCode, '-')  
WHERE ISNUMERIC(value) != 1 AND value != (SELECT Code FROM CompanyCodes)) AS T1 ON T1.RegistrationCode = p.RegistrationCode  
WHERE ISNUMERIC(value) = 1  
GROUP BY v2
