SET IDENTITY_INSERT ModuleTypes ON

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Patients')
BEGIN
    INSERT INTO ModuleTypes(Id, ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES (1, 'Patients', 'patients', 1, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Sales')
BEGIN
    INSERT INTO ModuleTypes(Id, ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES (2, 'Sales', 'sales', 2, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Management')
BEGIN
    INSERT INTO ModuleTypes(Id, ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES (3, 'Management', 'management', 4, 1)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Settings')
BEGIN
    INSERT INTO ModuleTypes(Id, ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES (4, 'Settings', 'settings', 6, 1)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Lab Results')
BEGIN
    INSERT INTO ModuleTypes(Id, ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES (5, 'Lab Results', 'folder', 3, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Reports')
BEGIN
    INSERT INTO ModuleTypes(Id, ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES (6, 'Reports', 'folder-reports', 5, 0)
END

SET IDENTITY_INSERT ModuleTypes OFF
GO