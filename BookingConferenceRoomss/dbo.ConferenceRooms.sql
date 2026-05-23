CREATE TABLE [dbo].[ConferenceRooms] (
    [Id]       INT           NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Capacity] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

