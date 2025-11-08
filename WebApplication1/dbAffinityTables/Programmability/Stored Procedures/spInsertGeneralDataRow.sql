IF OBJECT_ID('spInsertGeneralDataRow') IS NULL
    EXEC('CREATE PROCEDURE spInsertGeneralDataRow AS SET NOCOUNT ON;')
GO

ALTER PROC [dbo].[spInsertGeneralDataRow]
	@pFileName varchar(25),
	@pLink varchar(50),
	@pField01text varchar(100),
	@pField01comment varchar(50),
	@pField02text varchar(100),
	@pField02comment varchar(50)
AS
begin
	INSERT INTO MainDataTable
		(Id, SpreadsheetName, Link, Field1Data, Field1Meta, 
		Field2Data, Field2Meta, Field3Data, Field3Meta,	Field4Data, 
		Field4Meta,	EditDate, EditUser)
	Values(
		(SELECT COALESCE(max(Id)+1,0) from MainDataTable), -- max id + 1
		@pFileName,
		@pLink,
		@pField01text,
		@pField01comment,

		@pField02text,
		@pField02comment,
		null,
		null,
		null,

		null,
		getdate(),
		SUSER_NAME()
	);
end