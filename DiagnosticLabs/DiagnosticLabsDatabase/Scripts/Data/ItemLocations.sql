SET IDENTITY_INSERT ItemLocations ON

IF NOT EXISTS (SELECT * FROM ItemLocations WHERE ItemLocationName = 'STOCK ROOM')
BEGIN
    INSERT INTO ItemLocations(Id, ItemLocationName, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'STOCK ROOM', 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT ItemLocations OFF
GO