CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FIO] NTEXT NOT NULL, 
    [login] NVARCHAR(50) NOT NULL, 
    [password] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([id]) REFERENCES [Role]([id])
)
