SET IDENTITY_INSERT Items ON

IF NOT EXISTS (SELECT * FROM Items WHERE ItemName = 'TEST TUBE')
BEGIN
    INSERT INTO Items(Id, ItemName, Cost, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'TEST TUBE', 15.50, 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT Items OFF
GO