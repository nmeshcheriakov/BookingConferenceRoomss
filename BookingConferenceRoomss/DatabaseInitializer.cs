using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

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
                        catch (Exception)
                        {

                        }
                    }

                    
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

            
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                @"AttachDbFilename=" + dbPath + ";" +
                @"Integrated Security=True";

            // таблицы
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (!TableExists(connection, "Role"))
                    Execute(@"CREATE TABLE [dbo].[Role] ([Id] INT NOT NULL PRIMARY KEY, [name] NVARCHAR(50) NOT NULL)", connection);

                if (!TableExists(connection, "StatusBooking"))
                    Execute(@"CREATE TABLE [dbo].[StatusBooking] ([Id] INT NOT NULL PRIMARY KEY, [Name] NVARCHAR(50) NOT NULL)", connection);

                if (!TableExists(connection, "ConferenceRooms"))
                    Execute(@"CREATE TABLE [dbo].[ConferenceRooms] ([Id] INT NOT NULL PRIMARY KEY, [Name] NVARCHAR(50) NOT NULL, [Capacity] INT NOT NULL, [description] NVARCHAR(50) NULL, [image] IMAGE NULL)", connection);

                if (!TableExists(connection, "User"))
                    Execute(@"CREATE TABLE [dbo].[User] ([Id] INT NOT NULL PRIMARY KEY, [login] NVARCHAR(50) NOT NULL, [email] NVARCHAR(50) NOT NULL, [password] NVARCHAR(100) NOT NULL, [RoleId] INT NOT NULL, CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([Id]))", connection);

                if (!TableExists(connection, "Booking"))
                    Execute(@"CREATE TABLE [dbo].[Booking] ([Id] INT NOT NULL PRIMARY KEY, [CheckIn] DATETIME NOT NULL, [CheckOut] DATETIME NOT NULL, [Guests] INT NOT NULL, [UserId] INT NOT NULL, [ConferenceRoomId] INT NOT NULL, [StatusId] INT NOT NULL, CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]), CONSTRAINT [FK_Booking_ConferenceRooms] FOREIGN KEY ([ConferenceRoomId]) REFERENCES [dbo].[ConferenceRooms]([Id]), CONSTRAINT [FK_Booking_StatusBooking] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[StatusBooking]([Id]))", connection);


                TryExecute(@"IF NOT EXISTS (SELECT * FROM [Role] WHERE [Id] = 1) INSERT INTO [Role] ([Id], [name]) VALUES (1, N'Администратор')", connection);
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [Role] WHERE [Id] = 2) INSERT INTO [Role] ([Id], [name]) VALUES (2, N'Пользователь')", connection);

                TryExecute(@"IF NOT EXISTS (SELECT * FROM [StatusBooking] WHERE [Id] = 1) INSERT INTO [StatusBooking] ([Id], [Name]) VALUES (1, N'Подтверждено')", connection);
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [StatusBooking] WHERE [Id] = 2) INSERT INTO [StatusBooking] ([Id], [Name]) VALUES (2, N'Отменено')", connection);
                TryExecute(@"IF NOT EXISTS (SELECT * FROM [StatusBooking] WHERE [Id] = 3) INSERT INTO [StatusBooking] ([Id], [Name]) VALUES (3, N'Завершено')", connection);


                TryExecute(@"IF NOT EXISTS (SELECT * FROM [User] WHERE [login] = 'admin') 
                             INSERT INTO [User] ([Id], [email], [login], [password], [RoleId]) 
                             VALUES (1, N'admin@mail.ru', 'admin', 'temporary', 1)", connection);


                TryExecute(@"UPDATE [User] SET [password] = '$2a$11$qR7tF8vB5k9Z1X2y3M4u5OasGv7E6Pz8HwR3mN2o1CxBiGyDqFlSm' WHERE [login] = 'admin'", connection);

                // путь к изображению
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image");

                
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                    MessageBox.Show($"Создана папка для изображений: {basePath}\nПожалуйста, поместите файлы 1.jpg, 2.jpg, 3.jpg, 4.jpg, 5.jpg в эту папку",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Проверяем наличие файлов
                string[] imageFiles = new string[] { "1.jpg", "2.jpg", "3.jpg", "4.jpg", "5.jpg" };
                foreach (string file in imageFiles)
                {
                    string fullPath = Path.Combine(basePath, file);
                    if (!File.Exists(fullPath))
                    {
                        // Создаем заглушку - белый квадрат 200x200
                        CreateDummyImage(fullPath);
                    }
                }

                byte[] image1 = File.ReadAllBytes(Path.Combine(basePath, "1.jpg"));
                byte[] image2 = File.ReadAllBytes(Path.Combine(basePath, "2.jpg"));
                byte[] image3 = File.ReadAllBytes(Path.Combine(basePath, "3.jpg"));
                byte[] image4 = File.ReadAllBytes(Path.Combine(basePath, "4.jpg"));
                byte[] image5 = File.ReadAllBytes(Path.Combine(basePath, "5.jpg"));

                // вставка залов 
                void InsertRoom(int id, string name, int capacity, string desc, byte[] imgBytes)
                {
                    string sql = @"IF NOT EXISTS (SELECT * FROM [ConferenceRooms] WHERE [Id] = @id)
                                   INSERT INTO [ConferenceRooms] ([Id], [Name], [Capacity], [description], [image]) 
                                   VALUES (@id, @name, @cap, @desc, @img);";
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@cap", capacity);
                            cmd.Parameters.AddWithValue("@desc", desc);

                            
                            if (imgBytes != null && imgBytes.Length > 0)
                            {
                                cmd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = imgBytes;
                            }
                            else
                            {
                                cmd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = DBNull.Value;
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                       
                        Console.WriteLine($"Ошибка при вставке зала {name}: {ex.Message}");
                    }
                }

                // вставляем залы
                InsertRoom(1, "Малый зал «Никита»", 10, "Уютный зал для небольших совещаний и переговоров.", image1);
                InsertRoom(2, "Большой зал «Степа»", 50, "Просторный зал с проектором для проведения лекций.", image2);
                InsertRoom(3, "Президентский VIP-зал", 15, "Зал круглого стола с премиальным оборудованием.", image3);
                InsertRoom(4, "Зал суда", 25, "Для проведения судебных процессов над хулиганами", image4);
                InsertRoom(5, "Креативная зона «Йоу»", 8, "Пространство с пуфиками.", image5);

                
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [ConferenceRooms]", connection))
                    {
                        int count = (int)cmd.ExecuteScalar();
                        Console.WriteLine($"Добавлено залов: {count}");
                    }
                }
                catch { }
            }
        }

       
        private static void CreateDummyImage(string filePath)
        {
            try
            {
                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(200, 200))
                {
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                    {
                        g.Clear(System.Drawing.Color.White);
                        using (System.Drawing.Font font = new System.Drawing.Font("Arial", 12))
                        {
                            g.DrawString("Нет фото", font, System.Drawing.Brushes.Gray, new System.Drawing.PointF(50, 90));
                        }
                    }
                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch
            {
                
                File.WriteAllBytes(filePath, new byte[0]);
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