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
	Values(
		(SELECT max(Id)+1 from MainDataTable), -- max id + 1
		@pFileName,
		@pLink,
		@pField01text,
		@pField01comment,
		@pField02text,
		@pField02comment,
		null,
		null,
		null,
		null
	);
end