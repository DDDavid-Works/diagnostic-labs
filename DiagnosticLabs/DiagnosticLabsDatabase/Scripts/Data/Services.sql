SET IDENTITY_INSERT Services ON

IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Complete Blood Chemistry')
BEGIN
    INSERT INTO Services(Id, ServiceName, ServiceDescription, Price, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'Complete Blood Chemistry', 'Complete Blood Chemistry', 100, 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Services OFF
GO