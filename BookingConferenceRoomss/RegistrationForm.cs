using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookingConferenceRoomss
{
    public partial class RegistrationForm : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {


        }

        private void reg_bt_Click(object sender, EventArgs e)
        {
            string login = log_tb.Text.Trim();
            string email = email_tb.Text.Trim();
            string password = password_tb.Text.Trim();
            string repeatPassword = repeat_password_tb.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Шаг А: Проверяем, не занят ли уже такой логин или E-mail
                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE [Login] = @login OR [Email] = @email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@login", login);
                        checkCmd.Parameters.AddWithValue("@email", email);

                        int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (userExists > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином или E-mail уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Прерываем регистрацию
                        }
                    }

                    // Шаг Б: Если всё ок, добавляем нового пользователя
                    // По умолчанию ставим RoleId = 2 (обычный пользователь)
                    string insertQuery = "INSERT INTO [User] ([Login], [Password], [Email], [RoleId]) VALUES (@login, @password, @email, 2)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@login", login);
                        insertCmd.Parameters.AddWithValue("@password", password);
                        insertCmd.Parameters.AddWithValue("@email", email);

                        // ExecuteNonQuery используется для команд INSERT, UPDATE, DELETE
                        insertCmd.ExecuteNonQuery();
                    }

                    // Шаг В: Уведомляем об успехе и возвращаем на форму авторизации
                    MessageBox.Show("Регистрация успешно завершена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Открываем первую форму (авторизацию) обратно
                    Form1 loginForm = new Form1();
                    loginForm.Show();
                    this.Close(); // Закрываем форму регистрации насовсем (освобождаем память)
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] == null)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
            }
        }
    }
}
