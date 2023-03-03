CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL,
	[GameStateId] INT NULL
)

CREATE TABLE [dbo].[GameState]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [DidWon] BIT NOT NULL DEFAULT 0, 
    [UserId] INT NOT NULL,
    [Table] NCHAR(74) NULL DEFAULT '', 
    [Numbers] NCHAR(119) NULL DEFAULT '', 
    [NumberOfRolls] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [GameState_dbo.GameState_UserID] FOREIGN KEY ([UserId]) 
        REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
)

ALTER TABLE [dbo].[User]
ADD CONSTRAINT [User_dbo.User_GameStateID] FOREIGN KEY ([GameStateId]) 
        REFERENCES [dbo].[GameState] ([Id])
		
INSERT INTO [dbo].[User] (Name)
VALUES ('John Wick')