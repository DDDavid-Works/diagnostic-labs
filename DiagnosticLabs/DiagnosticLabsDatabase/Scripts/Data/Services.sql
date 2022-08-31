SET IDENTITY_INSERT Services ON

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Stool/Fecalysis')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'Stool/Fecalysis', 'Stool/Fecalysis', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Urinalysis')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (2, 'Urinalysis', 'Urinalysis', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Hematology')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (3, 'Hematology', 'Hematology', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Immunology')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (4, 'Immunology', 'Immunology', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Serology')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (5, 'Serology', 'Serology', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Pregnancy Test')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (6, 'Pregnancy Test', 'Pregnancy Test', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Clinical Chemistry')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (7, 'Clinical Chemistry', 'Clinical Chemistry', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Clinical Chemistry 1')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (8, 'Clinical Chemistry 1', 'Clinical Chemistry 1', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Clinical Chemistry 2')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (9, 'Clinical Chemistry 2', 'Clinical Chemistry 2', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Medical Examination')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (10, 'Medical Examination', 'Medical Examination', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Medical Examination Page 2')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (11, 'Medical Examination Page 2', 'Medical Examination Page 2', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Annual Physical Exam')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (12, 'Annual Physical Exam', 'Annual Physical Exam', 100, 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Annual Physical Exam Page 2')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (13, 'Annual Physical Exam Page 2', 'Annual Physical Exam Page 2', 100, 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Services OFF
GO