
Nilanjan Saha  2:25 PM
USE [FARDB]
GO
/****** Object: Table [dbo].[FARs] Script Date: 8/18/2022 10:54:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FARs] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [RequestId] NVARCHAR (2000) NULL,
    [Status]    INT             NULL,
    [CreatedBy] NVARCHAR (4000) NULL,
    [CreatedOn] DATE            NULL,
    [Summary]   NVARCHAR (4000) NULL,
    [Details]   NVARCHAR (4000) NULL
);  
