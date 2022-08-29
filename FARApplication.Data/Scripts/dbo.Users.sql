USE [FARDB]
GO

/****** Object: Table [dbo].[Users] Script Date: 8/29/2022 12:02:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (500) NOT NULL,
    [LastName]      VARCHAR (500) NOT NULL,
    [EmailId]       VARCHAR (500) NOT NULL,
    [Password]      VARCHAR (50)  NOT NULL,
    [ApprovalLevel] INT           NOT NULL,
    [IsActive]      INT           NULL
);


