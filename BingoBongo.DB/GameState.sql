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
