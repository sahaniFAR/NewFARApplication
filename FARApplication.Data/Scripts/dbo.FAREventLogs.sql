USE [FARDB]
GO

/****** Object: Table [dbo].[FAREventLogs] Script Date: 8/29/2022 12:02:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FAREventLogs] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [FARId]     INT             NOT NULL,
    [Message]   NVARCHAR (4000) NULL,
    [EventDate] DATETIME        NULL,
    [UserId]    INT             NULL
);


