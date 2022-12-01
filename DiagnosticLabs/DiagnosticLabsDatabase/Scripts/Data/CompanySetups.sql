SET IDENTITY_INSERT CompanySetups ON

IF NOT EXISTS (SELECT * FROM CompanySetups WHERE CompanyName = 'BIO ASSAY DIAGNOSTIC CENTER')
BEGIN
    INSERT INTO CompanySetups(Id, CompanyName, SubCompanyName, Tagline, Address, ContactNumbers, Email, Code, Logo, UpdatedByUserId, UpdatedDate)
    VALUES (1, 'BIO ASSAY DIAGNOSTIC CENTER', '', '', 'Angeles City', '888-8888', 'bio@yahoo.com', 'BADC', NULL, 1, GETDATE())
END

SET IDENTITY_INSERT CompanySetups OFF
GO