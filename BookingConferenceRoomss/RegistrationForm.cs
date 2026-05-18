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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Шаг А: Проверяем, не занят ли логин
                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE [Login] = @login";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@login", log_tb.Text.Trim());
                        int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (userExists > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // === ШАГ Б: ВЫЧИСЛЯЕМ СЛЕДУЮЩИЙ СВОБОДНЫЙ ID (Исправление ошибки) ===
                    int nextId = 1;
                    string idQuery = "SELECT ISNULL(MAX(Id), 0) + 1 FROM [User]";
                    using (SqlCommand idCmd = new SqlCommand(idQuery, connection))
                    {
                        nextId = Convert.ToInt32(idCmd.ExecuteScalar());
                    }
                    // =================================================================

                    // Шаг В: Вставляем данные. Обрати внимание, теперь [Id] и [email] на своих местах!
                    string insertQuery = "INSERT INTO [User] ([Id], [Login], [email], [Password], [RoleId]) VALUES (@id, @login, @email, @password, 2)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@id", nextId); // Передаем вычисленный ID
                        insertCmd.Parameters.AddWithValue("@login", log_tb.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@email", email_tb.Text.Trim()); // Если на форме нет поля Email, генерируем временный заглушечный адрес
                        insertCmd.Parameters.AddWithValue("@password", password_tb.Text.Trim());

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Регистрация успешно завершена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Возвращаемся на форму входа
                    Form1 loginForm = new Form1();
                    loginForm.Show();
                    this.Close();
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
