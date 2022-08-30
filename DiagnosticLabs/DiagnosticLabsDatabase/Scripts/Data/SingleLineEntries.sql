SET IDENTITY_INSERT SingleLineEntries ON

IF NOT EXISTS (SELECT * FROM SingleLineEntries WHERE ModuleId IS NULL AND FieldName = 'Gender' AND FieldValue = 'Male')
BEGIN
    INSERT INTO SingleLineEntries(Id, ModuleId, FieldName, FieldValue, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (1, NULL, 'Gender', 'Male', 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM SingleLineEntries WHERE ModuleId IS NULL AND FieldName = 'Gender' AND FieldValue = 'Female')
BEGIN
    INSERT INTO SingleLineEntries(Id, ModuleId, FieldName, FieldValue, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (2, NULL, 'Gender', 'Female', 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM SingleLineEntries WHERE ModuleId IS NULL AND FieldName = 'Civil Status' AND FieldValue = 'Single')
BEGIN
    INSERT INTO SingleLineEntries(Id, ModuleId, FieldName, FieldValue, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (3, NULL, 'Civil Status', 'Single', 1, 1, GETDATE(), 1, GETDATE())
END

IF NOT EXISTS (SELECT * FROM SingleLineEntries WHERE ModuleId IS NULL AND FieldName = 'Civil Status' AND FieldValue = 'Married')
BEGIN
    INSERT INTO SingleLineEntries(Id, ModuleId, FieldName, FieldValue, IsActive, CreatedByUserId, CreatedDate, UpdatedByUserId, UpdatedDate)
    VALUES (4, NULL, 'Civil Status', 'Married', 1, 1, GETDATE(), 1, GETDATE())
END

SET IDENTITY_INSERT SingleLineEntries OFF
GO