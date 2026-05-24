using System;
using MySql.Data.MySqlClient;

namespace LibraryApp
{
    class Program
    {
        
        static string connectionString = "Server=localhost;Database=books_db;Uid=root;Pwd=кщще;";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Библиотека - Управление книгами";

            // Проверяем подключение
            if (!TestConnection())
            {
                Console.WriteLine("Ошибка подключения к базе данных!");
                Console.WriteLine("Проверьте пароль в строке connectionString");
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Подключение к базе данных успешно!");

            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("МЕНЮ:");
                Console.WriteLine("1. Показать все книги");
                Console.WriteLine("2. Добавить книгу");
                Console.WriteLine("3. Удалить книгу");
                Console.WriteLine("4. Изменить книгу");
                Console.WriteLine("5. Выйти");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowAllBooks(); break;
                    case "2": AddBook(); break;
                    case "3": DeleteBook(); break;
                    case "4": UpdateBook(); break;
                    case "5":
                        Console.WriteLine("До свидания");
                        return;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        break;
                }

                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }

        
        static bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
        }

        //все книги
        static void ShowAllBooks()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT b.id, b.title, a.name AS author, g.name AS genre, b.year
                    FROM books b
                    JOIN authors a ON b.author_id = a.id
                    JOIN genres g ON b.genre_id = g.id
                    ORDER BY b.id;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Список книг пуст.");
                        return;
                    }

                    Console.WriteLine($"\n{"ID",-4} {"Название",-35} {"Автор",-25} {"Жанр",-15} {"Год",-6}");
                    Console.WriteLine(new string('-', 90));

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id"],-4} {reader["title"],-35} {reader["author"],-25} {reader["genre"],-15} {reader["year"],-6}");
                    }
                }
            }
        }

        //добавление
        static void AddBook()
        {
            

            
            Console.WriteLine("Список авторов:");
            ShowAuthors();

            Console.Write("\nВыберите ID автора: ");
            if (!int.TryParse(Console.ReadLine(), out int authorId))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            
            Console.WriteLine("\nСписок жанров:");
            ShowGenres();

            Console.Write("Выберите ID жанра: ");
            if (!int.TryParse(Console.ReadLine(), out int genreId))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            Console.Write("Название книги: ");
            string title = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Название не может быть пустым!");
                return;
            }

            Console.Write("Год издания: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Неверный год!");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO books (title, author_id, genre_id, year) 
                               VALUES (@title, @author, @genre, @year);";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", authorId);
                cmd.Parameters.AddWithValue("@genre", genreId);
                cmd.Parameters.AddWithValue("@year", year);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"\nКнига \"{title}\" успешно добавлена!");
            }
        }

        //удаление
        static void DeleteBook()
        {
            ShowAllBooks();
            Console.Write("\nВведите ID книги для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM books WHERE id = @id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nКнига удалена!");
            }
        }

        //изменить
        static void UpdateBook()
        {
            ShowAllBooks();
            Console.Write("\nВведите ID книги для изменения: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            Console.WriteLine("\nЧто хотите изменить?");
            Console.WriteLine("1. Только название");
            Console.WriteLine("2. Только год");
            Console.WriteLine("3. Всё вместе (название, год, автор, жанр)");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                switch (choice)
                {
                    case "1":
                        Console.Write("Новое название: ");
                        string newTitle = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(newTitle)) return;

                        string sql1 = "UPDATE books SET title = @title WHERE id = @id;";
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                        cmd1.Parameters.AddWithValue("@title", newTitle);
                        cmd1.Parameters.AddWithValue("@id", id);
                        cmd1.ExecuteNonQuery();
                        Console.WriteLine("\nНазвание обновлено!");
                        break;

                    case "2":
                        Console.Write("Новый год: ");
                        if (!int.TryParse(Console.ReadLine(), out int newYear)) return;

                        string sql2 = "UPDATE books SET year = @year WHERE id = @id;";
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        cmd2.Parameters.AddWithValue("@year", newYear);
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.ExecuteNonQuery();
                        Console.WriteLine("\nГод обновлён!");
                        break;

                    case "3":
                        Console.Write("Новое название: ");
                        string title = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(title)) return;

                        Console.Write("Новый год: ");
                        if (!int.TryParse(Console.ReadLine(), out int year)) return;

                        Console.WriteLine("\nСписок авторов:");
                        ShowAuthors();
                        Console.Write("👉 Новый ID автора: ");
                        if (!int.TryParse(Console.ReadLine(), out int authorId)) return;

                        Console.WriteLine("\nСписок жанров:");
                        ShowGenres();
                        Console.Write("Новый ID жанра: ");
                        if (!int.TryParse(Console.ReadLine(), out int genreId)) return;

                        string sql3 = @"UPDATE books SET title = @title, author_id = @author, 
                                        genre_id = @genre, year = @year WHERE id = @id;";
                        MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                        cmd3.Parameters.AddWithValue("@title", title);
                        cmd3.Parameters.AddWithValue("@author", authorId);
                        cmd3.Parameters.AddWithValue("@genre", genreId);
                        cmd3.Parameters.AddWithValue("@year", year);
                        cmd3.Parameters.AddWithValue("@id", id);
                        cmd3.ExecuteNonQuery();
                        Console.WriteLine("\nДанные книги обновлены!");
                        break;

                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        
        static void ShowAuthors()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT id, name FROM authors ORDER BY name;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"  {reader["id"]}. {reader["name"]}");
                    }
                }
            }
        }

        static void ShowGenres()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT id, name FROM genres ORDER BY name;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"  {reader["id"]}. {reader["name"]}");
                    }
                }
            }
        }
    }
}