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
                    
                    {
                        connection.Open();

                        // Проверка существования логина
                        string checkQuery = "SELECT COUNT(*) FROM [User] WHERE [login] = @login";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@login", log_tb.Text.Trim());
                            int userExists = (int)checkCmd.ExecuteScalar();

                            if (userExists > 0)
                            {
                                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (log_tb.Text == "" || email_tb.Text == "" || 
                                password_tb.Text == "" || repeat_password_tb.Text == "")
                            {
                                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (repeat_password_tb.Text != password_tb.Text)
                            {
                                MessageBox.Show("Пароли должны совпадать!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                            // Получаем следующий Id
                        int nextId;
                        string idQuery = "SELECT ISNULL(MAX(Id), 0) + 1 FROM [User]";
                        using (SqlCommand idCmd = new SqlCommand(idQuery, connection))
                        {
                            nextId = (int)idCmd.ExecuteScalar();
                        }

                        // Вставляем пользователя
                        string insertQuery = "INSERT INTO [User] ([Id], [login], [email], [password], [RoleId]) VALUES (@id, @login, @email, @password, 2)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@id", nextId);
                            insertCmd.Parameters.AddWithValue("@login", log_tb.Text.Trim()); // Используем логин как ФИО, если нет поля ФИО
                            insertCmd.Parameters.AddWithValue("@email", log_tb.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@password", password_tb.Text.Trim());
                            insertCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Регистрация успешно завершена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form1 loginForm = new Form1();
                        loginForm.Show();
                        this.Close();
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Выводим ВСЕ ошибки SQL
                    string allErrors = "";
                    foreach (SqlError error in sqlEx.Errors)
                    {
                        allErrors += $"Ошибка SQL: {error.Message}\nНомер: {error.Number}\n\n";
                    }
                    MessageBox.Show(allErrors, "Ошибка SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    string fullError = ex.Message;
                    Exception inner = ex.InnerException;
                    while (inner != null)
                    {
                        fullError += "\n→ " + inner.Message;
                        inner = inner.InnerException;
                    }
                    MessageBox.Show(fullError, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Back_bt_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }
    }
}
