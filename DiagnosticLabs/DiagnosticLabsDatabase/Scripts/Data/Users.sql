IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'Admin')
BEGIN
    INSERT INTO Users (Username, FullName, Password, IsActive, IsAdmin, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES ('Admin', 'Default User', 'admin', 1, 1, 1, GETDATE(), 1, GETDATE())
END