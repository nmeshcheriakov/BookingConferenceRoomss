CREATE TABLE [dbo].[Booking] (
    [Id]               INT      NOT NULL,
    [CheckIn]          DATETIME NOT NULL,
    [CheckOut]         DATETIME NOT NULL,
    [Guests]           INT      NOT NULL,
    [UserId]           INT      NOT NULL,
    [ConferenceRoomId] INT      NOT NULL,
    [StatusId]         INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Booking_ConferenceRooms] FOREIGN KEY ([ConferenceRoomId]) REFERENCES [dbo].[ConferenceRooms] ([Id]),
    CONSTRAINT [FK_Booking_StatusBooking] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[StatusBooking] ([Id])
);

