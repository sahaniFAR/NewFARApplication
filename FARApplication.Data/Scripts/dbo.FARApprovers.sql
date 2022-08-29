USE [FARDB]
GO

/****** Object: Table [dbo].[FARApprovers] Script Date: 8/29/2022 12:01:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FARApprovers] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       INT            NOT NULL,
    [FARId]        INT            NOT NULL,
    [Comments]     NVARCHAR (MAX) NULL,
    [ApprovedDate] DATETIME       NULL
);


