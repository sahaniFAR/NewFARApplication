USE [FARDB]
GO

/****** Object: Table [dbo].[FARs] Script Date: 8/29/2022 12:02:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FARs] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [RequestId] NVARCHAR (2000) NULL,
    [Status]    INT             NULL,
    [UserId]    INT             NOT NULL,
    [CreatedOn] DATE            NULL,
    [Summary]   NVARCHAR (4000) NULL,
    [Details]   NVARCHAR (4000) NULL
);


