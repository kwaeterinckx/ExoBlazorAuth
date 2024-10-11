﻿CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Login] NVARCHAR(50) UNIQUE NOT NULL,
	[Email] NVARCHAR(320) UNIQUE NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[IsAdmin] BIT DEFAULT 0 NOT NULL
)
