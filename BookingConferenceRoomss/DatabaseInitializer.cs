using System;
using System.Data.SqlClient;
using System.IO;

namespace BookingConferenceRooms
{
    public static class DatabaseInitializer
    {
        private static readonly string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;" +
            @"AttachDbFilename=|DataDirectory|\BookingConferenceRoomsDB.mdf;" +
            @"Integrated Security=True;" +
            @"Connect Timeout=30";

        public static void Initialize()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "BookingConferenceRoomsDB.mdf");

            // Если база данных уже существует — выходим
            if (File.Exists(dbPath))
            {
                return;
            }

            // Создаём базу данных
            string masterConnection =
                @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                @"Integrated Security=True";

            string createDbCommand =
                $@"CREATE DATABASE BookingConferenceRoomsDB
                   ON PRIMARY
                   (NAME = BookingConferenceRoomsDB,
                    FILENAME = '{dbPath}')
                   LOG ON
                   (NAME = BookingConferenceRoomsDB_log,
                    FILENAME = '{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookingConferenceRoomsDB_log.ldf")}')";

            using (SqlConnection connection = new SqlConnection(masterConnection))
            {
                connection.Open();

                // Создаём базу данных
                using (SqlCommand cmd = new SqlCommand(createDbCommand, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Создаём таблицы
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string[] createTableCommands = new string[]
                {
                    @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Role' AND xtype='U')
                    CREATE TABLE [dbo].[Role] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [Name] NVARCHAR(50) NOT NULL
                    );",

                    @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User' AND xtype='U')
                    CREATE TABLE [dbo].[User] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [Login] NVARCHAR(50)  NOT NULL,
                        [email] NVARCHAR(50) NOT NULL,
                        [Password] NVARCHAR(50) NOT NULL,
                        [RoleId] INT NULL,
                        CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([Id])
                    );",

                    @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StatusBooking' AND xtype='U')
                    CREATE TABLE [dbo].[StatusBooking] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [Name] NVARCHAR(50) NULL
                    );",

                    @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ConferenceRooms' AND xtype='U')
                    CREATE TABLE [dbo].[ConferenceRooms] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [Name] NVARCHAR(50) NULL,
                        [Capacity] INT NULL
                    );",

                    @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Booking' AND xtype='U')
                    CREATE TABLE [dbo].[Booking] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [CheckIn] DATETIME NULL,
                        [CheckOut] DATETIME NULL,
                        [Guests] INT NULL,
                        [UserId] INT NULL,
                        [ConferenceRoomId] INT NULL,
                        [StatusId] INT NULL,
                        CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),
                        CONSTRAINT [FK_Booking_ConferenceRooms] FOREIGN KEY ([ConferenceRoomId]) REFERENCES [dbo].[ConferenceRooms]([Id]),
                        CONSTRAINT [FK_Booking_StatusBooking] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[StatusBooking]([Id])
                    );",

                    // Заполняем таблицу ролей
                    @"IF NOT EXISTS (SELECT * FROM [dbo].[Role] WHERE [Id] = 1)
                    INSERT INTO [dbo].[Role] ([Id], [Name]) VALUES (1, 'Администратор');
                    IF NOT EXISTS (SELECT * FROM [dbo].[Role] WHERE [Id] = 2)
                    INSERT INTO [dbo].[Role] ([Id], [Name]) VALUES (2, 'Пользователь');",

                    // Заполняем статусы бронирования
                    @"IF NOT EXISTS (SELECT * FROM [dbo].[StatusBooking] WHERE [Id] = 1)
                    INSERT INTO [dbo].[StatusBooking] ([Id], [Name]) VALUES (1, 'Подтверждено');
                    IF NOT EXISTS (SELECT * FROM [dbo].[StatusBooking] WHERE [Id] = 2)
                    INSERT INTO [dbo].[StatusBooking] ([Id], [Name]) VALUES (2, 'Отменено');
                    IF NOT EXISTS (SELECT * FROM [dbo].[StatusBooking] WHERE [Id] = 3)
                    INSERT INTO [dbo].[StatusBooking] ([Id], [Name]) VALUES (3, 'Завершено');",

                    // Администратор по умолчанию (логин: admin, пароль: admin)
                    @"IF NOT EXISTS (SELECT * FROM [dbo].[User] WHERE [Login] = 'admin')
                    INSERT INTO [dbo].[User] ([Id], [Login], [Password], [RoleId]) VALUES (1, 'admin', 'admin', 1);"
                };

                foreach (string sql in createTableCommands)
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}