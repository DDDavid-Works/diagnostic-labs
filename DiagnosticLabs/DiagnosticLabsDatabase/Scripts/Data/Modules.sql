DECLARE @ModuleTypeId INT = 0

--REGISTRATION
SELECT TOP 1 @ModuleTypeId = Id FROM ModuleTypes WHERE ModuleTypeName = 'Registration'
IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Patients' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Patients', 1, 1, 1, 1, 1, 1, 0, 'patients', 1, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Companies' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Companies', 1, 1, 1, 1, 1, 1, 0, 'company', 3, 1)
END

--SALES
SELECT TOP 1 @ModuleTypeId = Id FROM ModuleTypes WHERE ModuleTypeName = 'Sales'
IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Payment' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Payment', 1, 1, 1, 1, 1, 1, 0, 'payment', 1, 1)
END

--LAB RESULTS
SELECT TOP 1 @ModuleTypeId = Id FROM ModuleTypes WHERE ModuleTypeName = 'Lab Results'
IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Stool/Fecalysis' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Stool/Fecalysis', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 1, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Urinalysis' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Urinalysis', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 2, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Hematology' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Hematology', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 3, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Immunology' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Immunology', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 4, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Serology' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Serology', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 5, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Pregnancy Test' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Pregnancy Test', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 6, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Clinical Chemistry' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Clinical Chemistry', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 7, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Clinical Chemistry 1' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Clinical Chemistry 1', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 8, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Clinical Chemistry 2' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Clinical Chemistry 2', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 9, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Medical Examination' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Medical Examination', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 10, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Medical Examination Page 2' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Medical Examination Page 2', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 11, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Annual Physical Exam' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Annual Physical Exam', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 12, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Annual Physical Exam Page 2' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Annual Physical Exam Page 2', 1, 1, 1, 1, 1, 1, 0, 'lab-result', 13, 1)
END

--MANAGEMENT
SELECT TOP 1 @ModuleTypeId = Id FROM ModuleTypes WHERE ModuleTypeName = 'Management'
IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Departments' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Departments', 1, 1, 1, 1, 1, 1, 1, 'departments', 1, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Services' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Services', 1, 1, 1, 1, 1, 1, 0, 'services', 2, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Packages' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Packages', 1, 1, 1, 1, 1, 1, 0, 'package', 4, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Items' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Items', 1, 1, 1, 1, 1, 1, 0, 'item', 5, 0)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Item Locations' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Item Locations', 1, 1, 1, 1, 1, 1, 0, 'location', 6, 0)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Discounts' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Discounts', 1, 1, 1, 1, 1, 1, 0, 'discount', 7, 1)
END

--REPORTS
SELECT TOP 1 @ModuleTypeId = Id FROM ModuleTypes WHERE ModuleTypeName = 'Reports'
IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Sales Report' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Sales Report', 0, 1, 0, 0, 0, 1, 0, 'report', 1, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Statement of Accounts' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Statement of Accounts', 0, 1, 0, 0, 1, 1, 0, 'report', 2, 1)
END

--SETTINGS
SELECT TOP 1 @ModuleTypeId = Id FROM ModuleTypes WHERE ModuleTypeName = 'Settings'
IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Company Setup' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Company Setup', 0, 1, 1, 0, 0, 0, 0, 'config', 1, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Users' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Users', 1, 1, 1, 1, 1, 0, 0, 'user', 2, 1)
END

IF NOT EXISTS (SELECT * FROM Modules WHERE ModuleName = 'Change Password' AND ModuleTypeId = @ModuleTypeId)
BEGIN
    INSERT INTO Modules(ModuleTypeId, ModuleName, HasCreate, HasView, HasEdit, HasDelete, HasSearch, HasPrint, HasShowList, Icon, SortOrder, IsActive)
    VALUES (@ModuleTypeId, 'Change Password', 0, 1, 1, 0, 1, 0, 0, 'permission', 3, 1)
END
