/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r Data\ModuleTypes.sql
:r Data\Modules.sql
:r Data\CompanySetups.sql
:r Data\Users.sql
:r Data\UserPermissions.sql
:r Data\Items.sql
:r Data\ItemLocations.sql
:r Data\Companies.sql
:r Data\Services.sql
:r Data\Departments.sql
:r Data\SingleLineEntries.sql
