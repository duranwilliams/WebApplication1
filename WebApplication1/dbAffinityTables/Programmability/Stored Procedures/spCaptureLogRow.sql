--select * from MainDataTable;

IF OBJECT_ID('spCaptureLogRow') IS NULL
    EXEC('CREATE PROCEDURE spCaptureLogRow AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE [dbo].[spCaptureLogRow]
	@InnerException varchar(100)
AS
BEGIN
	INSERT INTO dbo.Logger
	Values((select COALESCE(max(id)+1,0) from dbo.Logger), @InnerException, getdate());
END