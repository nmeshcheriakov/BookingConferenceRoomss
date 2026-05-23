CREATE TABLE [dbo].[ConferenceRooms] (
    [Id]       INT           NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Capacity] INT           NOT NULL,
    [description] NCHAR(10) NULL, 
    [image] IMAGE NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

