SET IDENTITY_INSERT Companies ON

IF NOT EXISTS (SELECT * FROM Companies WHERE CompanyName = 'WALK-IN')
BEGIN
    INSERT INTO Companies(Id, CompanyName, Address, ContactNumbers, ContactPerson, IsSystem, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (0, 'WALK-IN', 'WALK-IN', '', '', 1, 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Companies OFF
GO