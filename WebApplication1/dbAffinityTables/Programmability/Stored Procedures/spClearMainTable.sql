IF OBJECT_ID('spClearMainTable') IS NULL
    EXEC('CREATE PROCEDURE spClearMainTable AS SET NOCOUNT ON;')
GO


ALTER PROCEDURE [dbo].[spClearMainTable]
	@deleteAll bit = 0
AS
	Delete from MainDataTable
	where @deleteAll = 1;
RETURN 0
