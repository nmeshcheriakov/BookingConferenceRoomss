using System;
using System.Data.SqlClient;
using System.IO;

namespace BookingConferenceRooms
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookingConferenceRoomsDB.mdf");
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookingConferenceRoomsDB_log.ldf");

            string masterConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";

            // Создаём базу данных, только если файл ещё не существует
            if (!File.Exists(dbPath))
            {
                using (SqlConnection connection = new SqlConnection(masterConnection))
                {
                    connection.Open();

                    // Удаляем базу из памяти сервера, если она там есть
                    string dropDb = @"
                        IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BookingConferenceRoomsDB')
                        BEGIN
                            ALTER DATABASE [BookingConferenceRoomsDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                            DROP DATABASE [BookingConferenceRoomsDB];
                        END";
                    using (SqlCommand cmd = new SqlCommand(dropDb, connection))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception) {

                        }
                    }

                    // Создаём базу заново
                    string createDb = $@"
                        CREATE DATABASE [BookingConferenceRoomsDB]
                        ON PRIMARY (NAME = N'BookingConferenceRoomsDB', FILENAME = N'{dbPath}')
                        LOG ON (NAME = N'BookingConferenceRoomsDB_log', FILENAME = N'{logPath}')";
                    using (SqlCommand cmd = new SqlCommand(createDb, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Строка подключения к базе
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                @"AttachDbFilename=" + dbPath + ";" +
                @"Integrated Security=True";

            // Создаём таблицы
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (!TableExists(connection, "Role"))
                    Execute(@"CREATE TABLE [dbo].[Role] ([Id] INT NOT NULL PRIMARY KEY, [name] NVARCHAR(50) NOT NULL)", connection);

                if (!TableExists(connection, "StatusBooking"))
                    Execute(@"CREATE TABLE [dbo].[StatusBooking] ([Id] INT NOT NULL PRIMARY KEY, [Name] NVARCHAR(50) NOT NULL)", connection);

                if (!TableExists(connection, "ConferenceRooms"))
                    Execute(@"CREATE TABLE [dbo].[ConferenceRooms] ([Id] INT NOT NULL PRIMARY KEY, [Name] NVARCHAR(50) NOT NULL, [Capacity] INT NOT NULL, [description] NVARCHAR(50) NULL, [image] IMAGE NULL)", connection);

                // ВАЖНО: Изменили NVARCHAR(50) на NVARCHAR(100) для поля [password], чтобы хэш BCrypt помещался целиком!
                if (!TableExists(connection, "User"))
                    Execute(@"CREATE TABLE [dbo].[User] ([Id] INT NOT NULL PRIMARY KEY, [login] NVARCHAR(50) NOT NULL, [email] NVARCHAR(50) NOT NULL, [password] NVARCHAR(100) NOT NULL, [RoleId] INT NOT NULL, CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([Id]))", connection);

                if (!TableExists(connection, "Booking"))
                    Execute(@"CREATE TABLE [dbo].[Booking] ([Id] INT NOT NULL PRIMARY KEY, [CheckIn] DATETIME NOT NULL, [CheckOut] DATETIME NOT NULL, [Guests] INT NOT NULL, [UserId] INT NOT NULL, [ConferenceRoomId] INT NOT NULL, [StatusId] INT NOT NULL, CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]), CONSTRAINT [FK_Booking_ConferenceRooms] FOREIGN KEY ([ConferenceRoomId]) REFERENCES [dbo].[ConferenceRooms]([Id]), CONSTRAINT [FK_Booking_StatusBooking] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[StatusBooking]([Id]))", connection);

                // --- НАЧАЛЬНЫЕ СПРАВОЧНЫЕ ДАННЫЕ ---
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [Role] WHERE [Id] = 1) INSERT INTO [Role] ([Id], [name]) VALUES (1, N'Администратор')", connection);
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [Role] WHERE [Id] = 2) INSERT INTO [Role] ([Id], [name]) VALUES (2, N'Пользователь')", connection);

                TryExecute(@"IF NOT EXISTS (SELECT * FROM [StatusBooking] WHERE [Id] = 1) INSERT INTO [StatusBooking] ([Id], [Name]) VALUES (1, N'Подтверждено')", connection);
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [StatusBooking] WHERE [Id] = 2) INSERT INTO [StatusBooking] ([Id], [Name]) VALUES (2, N'Отменено')", connection);
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [StatusBooking] WHERE [Id] = 3) INSERT INTO [StatusBooking] ([Id], [Name]) VALUES (3, N'Завершено')", connection);

                // 1. Если админа нет — создаем его
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [User] WHERE [login] = 'admin') 
                             INSERT INTO [User] ([Id], [email], [login], [password], [RoleId]) 
                             VALUES (1, N'admin@mail.ru', 'admin', 'temporary', 1)", connection);

                // 2. ЖЕСТКО обновляем ему хэш пароля на правильный (в одну строку, без переносов!)
                TryExecute(@"UPDATE [User] SET [password] = '$2a$11$qR7tF8vB5k9Z1X2y3M4u5OasGv7E6Pz8HwR3mN2o1CxBiGyDqFlSm' WHERE [login] = 'admin'", connection);

                // --- ДОБАВЛЕНИЕ ТЕСТОВЫХ КОНФЕРЕНЦ-ЗАЛОВ ---
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [ConferenceRooms] WHERE [Id] = 1) 
                             INSERT INTO [ConferenceRooms] ([Id], [Name], [Capacity], [description]) 
                             VALUES (1, N'Малый зал «Альфа»', 10, N'Уютный зал для небольших совещаний и переговоров.')", connection);

                TryExecute(@"IF NOT EXISTS (SELECT * FROM [ConferenceRooms] WHERE [Id] = 2) 
                             INSERT INTO [ConferenceRooms] ([Id], [Name], [Capacity], [description]) 
                             VALUES (2, N'Большой зал «Омега»', 50, N'Просторный зал с проектором для проведения лекций.')", connection);

                TryExecute(@"IF NOT EXISTS (SELECT * FROM [ConferenceRooms] WHERE [Id] = 3) 
                             INSERT INTO [ConferenceRooms] ([Id], [Name], [Capacity], [description]) 
                             VALUES (3, N'Президентский VIP-зал', 15, N'Зал круглого стола с премиальным оборудованием.')", connection);

                // --- НОВАЯ СТРОКА: ДОБАВЛЕНИЕ ДАННЫХ О ДАТЕ И ВРЕМЕНИ БРОНИРОВАНИЯ ---
                // Добавляем тестовое бронирование №1 для зала Альфа (Id=1), пользователя Администратор (Id=1), статус Подтверждено (Id=1)
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [Booking] WHERE [Id] = 1) 
                             INSERT INTO [Booking] ([Id], [CheckIn], [CheckOut], [Guests], [UserId], [ConferenceRoomId], [StatusId]) 
                             VALUES (1, '2026-06-15 10:00:00', '2026-06-15 12:30:00', 5, 1, 1, 1)", connection);

                // Добавляем тестовое бронирование №2 для зала Омега (Id=2)
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [Booking] WHERE [Id] = 2) 
                             INSERT INTO [Booking] ([Id], [CheckIn], [CheckOut], [Guests], [UserId], [ConferenceRoomId], [StatusId]) 
                             VALUES (2, '2026-06-16 14:00:00', '2026-06-16 18:00:00', 30, 1, 2, 1)", connection);
            }
        }

        private static bool TableExists(SqlConnection connection, string tableName)
        {
            string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @name";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@name", tableName);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private static void Execute(string sql, SqlConnection connection)
        {
            new SqlCommand(sql, connection).ExecuteNonQuery();
        }

        private static void TryExecute(string sql, SqlConnection connection)
        {
            try { new SqlCommand(sql, connection).ExecuteNonQuery(); } catch { }
        }
    }
}