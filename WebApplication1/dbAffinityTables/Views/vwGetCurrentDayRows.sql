IF NOT EXISTS (
    SELECT * 
    FROM sys.views 
    WHERE name = 'vwGetCurrentDayRows' 
)
BEGIN
EXEC sp_executesql 
    '
    -- The view does NOT exist, so we create it
    CREATE VIEW vwGetCurrentDayRows
    AS
    SELECT 
        ColumnA, 
        ColumnB
    FROM 
        dbo.MainDataTable
        '
        
END
ELSE
BEGIN
    -- The view DOES exist, so we usually just skip, 
    -- or you could choose to ALTER it here.
    PRINT 'View vwGetCurrentDayRows already exists.';
END
GO

ALTER VIEW [dbo].[vwGetCurrentDayRows]
	AS  

	SELECT
    *
FROM
    MainDataTable
WHERE
    EditDate >= CAST(GETDATE() AS DATE) -- Starts at 00:00:00 today
    AND EditDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE)); -- Ends just before 00:00:00 tomorrow
