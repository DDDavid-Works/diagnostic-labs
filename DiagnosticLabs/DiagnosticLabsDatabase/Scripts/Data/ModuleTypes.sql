IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Registration')
BEGIN
    INSERT INTO ModuleTypes(ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES ('Registration', 'registration', 1, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Sales')
BEGIN
    INSERT INTO ModuleTypes(ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES ('Sales', 'sales', 2, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Lab Results')
BEGIN
    INSERT INTO ModuleTypes(ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES ('Lab Results', 'folder', 3, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Management')
BEGIN
    INSERT INTO ModuleTypes(ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES ('Management', 'management', 4, 1)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Reports')
BEGIN
    INSERT INTO ModuleTypes(ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES ('Reports', 'folder-reports', 5, 0)
END

IF NOT EXISTS (SELECT * FROM ModuleTypes WHERE ModuleTypeName = 'Settings')
BEGIN
    INSERT INTO ModuleTypes(ModuleTypeName, Icon, SortOrder, IsAdmin)
    VALUES ('Settings', 'settings', 6, 1)
END
