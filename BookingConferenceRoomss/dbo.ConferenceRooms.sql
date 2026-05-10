CREATE TABLE [dbo].[ConferenceRooms]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] NTEXT NOT NULL, 
    [capacity] INT NOT NULL, 
    [price] MONEY NOT NULL, 
    [description] NVARCHAR(50) NULL, 
    [photo] IMAGE NULL
)
