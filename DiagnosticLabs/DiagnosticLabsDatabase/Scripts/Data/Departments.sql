SET IDENTITY_INSERT Departments ON

IF NOT EXISTS (SELECT * FROM Departments WHERE DepartmentName = 'Physical Exam')
BEGIN
    INSERT INTO Departments(Id, DepartmentName, DepartmentDescription, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'Physical Exam', 'Physical Exam', 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM Departments WHERE DepartmentName = 'Chemistry Lab')
BEGIN
    INSERT INTO Departments(Id, DepartmentName, DepartmentDescription, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (2, 'Chemistry Lab', 'Chemistry Lab', 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Departments OFF
GO