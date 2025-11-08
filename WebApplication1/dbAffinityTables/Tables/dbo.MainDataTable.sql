USE [AffinityTables]
GO

/****** Object: Table [dbo].[MainDataTable] Script Date: 11/8/2025 3:53:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[MainDataTable];


GO
CREATE TABLE [dbo].[MainDataTable] (
    [Id]              INT           NOT NULL,
    [SpreadsheetName] VARCHAR (50)  NULL,
    [Link]            VARCHAR (MAX) NULL,
    [Field1Data]      NCHAR (10)    NULL,
    [Field1Meta]      NCHAR (10)    NULL,
    [Field2Data]      NCHAR (10)    NULL,
    [Field2Meta]      NCHAR (10)    NULL,
    [Field3Data]      NCHAR (10)    NULL,
    [Field3Meta]      NCHAR (10)    NULL,
    [Field4Data]      NCHAR (10)    NULL,
    [Field4Meta]      NCHAR (10)    NULL,
    [EditDate]        DATETIME      NOT NULL,
    [EditUser]        VARCHAR (35)  NOT NULL
);


