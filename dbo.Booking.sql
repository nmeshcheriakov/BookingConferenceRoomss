CREATE TABLE [dbo].[Booking]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [datetime_check-in] DATETIME NOT NULL, 
    [datetime_check-out] DATETIME NOT NULL, 
    [number_guests] INT NOT NULL, 
    CONSTRAINT [FK_Booking_User] FOREIGN KEY ([id]) REFERENCES [User]([id]), 
    CONSTRAINT [FK_Booking_StatusBooking] FOREIGN KEY ([id]) REFERENCES [StatusBooking]([id]), 
    CONSTRAINT [FK_Booking_ConferenceRooms] FOREIGN KEY ([id]) REFERENCES [ConferenceRooms]([id])
)
