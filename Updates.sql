IF NOT EXISTS(SELECT 1 FROM DocumentType)
BEGIN
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Visa', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Green Card', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'PassPort', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'License', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Photo Identity', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'I-9 Form', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Paystub', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'W2 Copy', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Educational Certificates', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Resume', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Invoices', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Contracts', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Insurance Certificates', 1
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Interview Video', 2
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Interview Feedback', 2
	INSERT INTO DOCUMENTTYPE(Name, DocumentGroup) SELECT 'Interview Qns & Excercise Files', 2
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'Profile Picture', 1
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'EAD Copy', 1
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'Agreement', 3
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'W9', 4
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'Insurance Certificates', 4
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'Contracts', 4
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'Purchase Order', 1
	INSERT INTO DocumentType(Name, DocumentGroup) SELECT 'Immigration Documents', 1
END
GO

IF NOT EXISTS(SELECT 1 FROM ResourceType)
BEGIN
	INSERT INTO ResourceType(Name) SELECT 'W2 Employees'
	INSERT INTO ResourceType(Name) SELECT 'Corp to Corp'
	INSERT INTO ResourceType(Name) SELECT '1099'
END
GO

IF NOT EXISTS(SELECT 1 FROM SkillSet)
BEGIN
	INSERT INTO SkillSet(Name, Type) SELECT 'Asp.Net',1
	INSERT INTO SkillSet(Name, Type) SELECT 'C#',1
	INSERT INTO SkillSet(Name, Type) SELECT 'Sql Server 2005',1
	INSERT INTO SkillSet(Name, Type) SELECT 'Sql Server 2008',1
	INSERT INTO SkillSet(Name, Type) SELECT 'Sql Server 2012',1
	INSERT INTO SkillSet(Name, Type) SELECT 'MVC5',1
	INSERT INTO SkillSet(Name, Type) SELECT 'SQL Server Reporting Services(SSRS)',1
	INSERT INTO SkillSet(Name, Type) SELECT 'Jquery',1
END
GO