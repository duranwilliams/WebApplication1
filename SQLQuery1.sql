USE [AffinityTables]
GO
/*
DECLARE	@return_value Int

EXEC	@return_value = [dbo].[spGetMainTableData]
		@param2 = NULL

SELECT	@return_value as 'Return Value'

GO
*/

insert into MainDataTable
values(
	1, 'Name', 'Desc'
)
