CREATE VIEW [dbo].[LabResults]
AS
SELECT ROW_NUMBER() OVER (ORDER BY Id) RowNumber, * FROM
(
    SELECT 'Stool/Fecalysis' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, l.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', l.CompanyOrPhysician AS Company, l.DateRequested, l.IsActive 
    FROM StoolFecalyses l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId

    UNION

    SELECT 'Urinalysis' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, l.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', l.CompanyOrPhysician AS Company, l.DateRequested, l.IsActive 
    FROM Urinalyses l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId

    UNION

    SELECT 'Annual Physical Exam' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateInputted AS 'DateRequested', l.IsActive 
    FROM APEs l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Medical Examination' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateInputted AS 'DateRequested', l.IsActive 
    FROM MERs l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Clinical Chemistry' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateRequested, l.IsActive 
    FROM ClinicalChemistries l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Clinical Chemistry 1' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateRequested, l.IsActive 
    FROM ClinicalChemistries1 l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Clinical Chemistry 2' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateRequested, l.IsActive 
    FROM ClinicalChemistries2 l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Immunology' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateRequested, l.IsActive 
    FROM Immunologies l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Pregnancy Test' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateRequested, l.IsActive 
    FROM PregnancyTests l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

    UNION

    SELECT 'Serology' AS Service, l.Id, l.PatientRegistrationId, l.PatientId, p.PatientCode, l.PatientName, pr.CompanyId AS 'CompanyId', c.CompanyName AS Company, l.DateRequested, l.IsActive 
    FROM Serologies l
    LEFT OUTER JOIN PatientRegistrations pr ON pr.Id = l.PatientRegistrationId
    LEFT OUTER JOIN Patients p ON p.Id = l.PatientId
    LEFT OUTER JOIN Companies c ON c.Id = pr.CompanyId

) AS Data