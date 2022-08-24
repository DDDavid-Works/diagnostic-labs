SET IDENTITY_INSERT Companies ON

IF NOT EXISTS (SELECT * FROM Companies WHERE CompanyName = 'SM CLARK 001')
BEGIN
    INSERT INTO Companies(Id, CompanyName, Address, ContactNumbers, ContactPerson, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'SM CLARK 001', 'SM CLARK 001', '0000001', 'MR. SY', 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Companies OFF
GO