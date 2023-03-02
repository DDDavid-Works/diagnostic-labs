DECLARE @ServiceId BIGINT
DECLARE @ServiceName VARCHAR(200)

DECLARE db_cursor CURSOR FOR 
SELECT Id FROM Services

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @ServiceId
WHILE @@FETCH_STATUS = 0  
BEGIN
	UPDATE Modules SET ServiceId = @ServiceId
	WHERE ModuleName = (SELECT ServiceName FROM Services WHERE Id = @ServiceId)

	FETCH NEXT FROM db_cursor INTO @ServiceId
END 
CLOSE db_cursor  
DEALLOCATE db_cursor 
