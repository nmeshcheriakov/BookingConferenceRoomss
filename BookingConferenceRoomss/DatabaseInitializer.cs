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
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookingConferenceRoomsDB.mdf");

            // Если файл базы данных уже физически существует на диске — выходим, база на месте
            if (File.Exists(dbPath))
            {
                return;
            }

            // Если файлов нет, очищаем системный кэш LocalDB через отсоединение (DETACH)
            string masterConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(masterConnection))
            {
                try
                {
                    connection.Open();

                    // Безопасное «забывание» старой базы. Если файлов нет, этот скрипт просто выкинет имя из памяти LocalDB
                    string detachCmdText = @"
                        IF EXISTS (SELECT * FROM sys.databases WHERE name = 'BookingConferenceRoomsDB')
                        BEGIN
                            ALTER DATABASE BookingConferenceRoomsDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                            EXEC sp_detach_db 'BookingConferenceRoomsDB', 'true';
                        END";

                    using (SqlCommand detachCmd = new SqlCommand(detachCmdText, connection))
                    {
                        try { detachCmd.ExecuteNonQuery(); } catch { /* Игнорируем внутренние капризы LocalDB */ }
                    }

                    // Теперь создаем базу абсолютно чисто
                    string createDbCommand = $@"
                        CREATE DATABASE BookingConferenceRoomsDB
                        ON PRIMARY
                        (NAME = BookingConferenceRoomsDB, FILENAME = '{dbPath}')
                        LOG ON
                        (NAME = BookingConferenceRoomsDB_log, FILENAME = '{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookingConferenceRoomsDB_log.ldf")}')";

                    using (SqlCommand createCmd = new SqlCommand(createDbCommand, connection))
                    {
                        createCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Критическая ошибка при создании базы данных в LocalDB: {ex.Message}", ex);
                }
            }

            // 3. Создаём таблицы (этот блок оставляем без изменений)
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // 1. Role
                string createRole = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Role' AND xtype='U')
                    CREATE TABLE [dbo].[Role] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [name] NTEXT NOT NULL
                    );";
                ExecuteCommand(createRole, connection);

                // 2. StatusBooking
                string createStatus = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StatusBooking' AND xtype='U')
                    CREATE TABLE [dbo].[StatusBooking] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [Name] NVARCHAR(50) NOT NULL
                    );";
                ExecuteCommand(createStatus, connection);

                // 3. ConferenceRooms
                string createRooms = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ConferenceRooms' AND xtype='U')
                    CREATE TABLE [dbo].[ConferenceRooms] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [Name] NVARCHAR(50) NOT NULL,
                        [Capacity] INT NOT NULL,
                        [description] NCHAR(10) NULL,
                        [image] IMAGE NULL
                    );";
                ExecuteCommand(createRooms, connection);

                // 4. User
                string createUser = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User' AND xtype='U')
                    CREATE TABLE [dbo].[User] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [FIO] NTEXT NOT NULL,
                        [login] NVARCHAR(50) NOT NULL,
                        [password] NVARCHAR(50) NOT NULL,
                        [RoleId] INT NULL,
                        CONSTRAINT [FK_User_Role] FOREIGN KEY ([Id]) REFERENCES [dbo].[Role]([Id])
                    );";
                ExecuteCommand(createUser, connection);

                // 5. Booking
                string createBooking = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Booking' AND xtype='U')
                    CREATE TABLE [dbo].[Booking] (
                        [Id] INT NOT NULL PRIMARY KEY,
                        [CheckIn] DATETIME NOT NULL,
                        [CheckOut] DATETIME NOT NULL,
                        [Guests] INT NOT NULL,
                        [UserId] INT NOT NULL,
                        [ConferenceRoomId] INT NOT NULL,
                        [StatusId] INT NOT NULL,
                        CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),
                        CONSTRAINT [FK_Booking_ConferenceRooms] FOREIGN KEY ([ConferenceRoomId]) REFERENCES [dbo].[ConferenceRooms]([Id]),
                        CONSTRAINT [FK_Booking_StatusBooking] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[StatusBooking]([Id])
                    );";
                ExecuteCommand(createBooking, connection);

                // Заполняем начальные данные

                // Роли
                string insertRoles = @"
                    IF NOT EXISTS (SELECT * FROM [dbo].[Role] WHERE [Id] = 1)
                    INSERT INTO [dbo].[Role] ([Id], [name]) VALUES (1, N'Администратор');
                    IF NOT EXISTS (SELECT * FROM [dbo].[Role] WHERE [Id] = 2)
                    INSERT INTO [dbo].[Role] ([Id], [name]) VALUES (2, N'Пользователь');";
                ExecuteCommand(insertRoles, connection);

                // Статусы
                string insertStatuses = @"
                    IF NOT EXISTS (SELECT * FROM [dbo].[StatusBooking] WHERE [Id] = 1)
                    INSERT INTO [dbo].[StatusBooking] ([Id], [Name]) VALUES (1, N'Подтверждено');
                    IF NOT EXISTS (SELECT * FROM [dbo].[StatusBooking] WHERE [Id] = 2)
                    INSERT INTO [dbo].[StatusBooking] ([Id], [Name]) VALUES (2, N'Отменено');
                    IF NOT EXISTS (SELECT * FROM [dbo].[StatusBooking] WHERE [Id] = 3)
                    INSERT INTO [dbo].[StatusBooking] ([Id], [Name]) VALUES (3, N'Завершено');";
                ExecuteCommand(insertStatuses, connection);

                // Администратор по умолчанию
                string insertAdmin = @"
                    IF NOT EXISTS (SELECT * FROM [dbo].[User] WHERE [login] = 'admin')
                    INSERT INTO [dbo].[User] ([Id], [FIO], [login], [password], [RoleId]) 
                    VALUES (1, N'Администратор', 'admin', 'admin', 1);";
                ExecuteCommand(insertAdmin, connection);
            }
        }

        private static void ExecuteCommand(string sql, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
            