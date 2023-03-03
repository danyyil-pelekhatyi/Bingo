CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL,
	[GameStateId] INT NULL, 
	CONSTRAINT [User_dbo.User_GameStateID] FOREIGN KEY ([GameStateId]) 
        REFERENCES [dbo].[GameState] ([Id])
)
