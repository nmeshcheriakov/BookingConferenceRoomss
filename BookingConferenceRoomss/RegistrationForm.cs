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
using BCrypt.Net;
using System.Security.Cryptography;

namespace BookingConferenceRoomss
{
    public partial class RegistrationForm : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;
        private bool isBackButtonClicked = false;

        public RegistrationForm()
        {
            InitializeComponent();
            this.FormClosing += RegistrationForm_FormClosing;
        }

        private void RegistrationForm_Load(object sender, EventArgs e) { }
        private void reg_bt_Click(object sender, EventArgs e)
        {
            string login = log_tb.Text.Trim();
            string email = email_tb.Text.Trim();
            string password = password_tb.Text.Trim();
            string repeatPassword = repeat_password_tb.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли должны совпадать!", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE [login] = @login";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@login", login);
                        int userExists = (int)checkCmd.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    int nextId;
                    string idQuery = "SELECT ISNULL(MAX(Id), 0) + 1 FROM [User]";
                    using (SqlCommand idCmd = new SqlCommand(idQuery, connection))
                    {
                        nextId = (int)idCmd.ExecuteScalar();
                    }

                    //хэш пароля
                    string securePasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

                    string insertQuery = "INSERT INTO [User] ([Id], [login], [email], [password], [RoleId]) VALUES (@id, @login, @email, @password, 2)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@id", nextId);
                        insertCmd.Parameters.AddWithValue("@login", login);
                        insertCmd.Parameters.AddWithValue("@email", email);
                        insertCmd.Parameters.AddWithValue("@password", securePasswordHash);
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Регистрация успешно завершена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    isBackButtonClicked = true;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    string allErrors = "";
                    foreach (SqlError error in sqlEx.Errors)
                    {
                        allErrors += $"Ошибка SQL: {error.Message}\nНомер: {error.Number}\n\n";
                    }
                    MessageBox.Show(allErrors, "Ошибка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Критическая ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Back_bt_Click(object sender, EventArgs e)
        {
            isBackButtonClicked = true;
            this.Close();
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Form1)
                {
                    Form1 loginForm = (Form1)form;
                    loginForm.ClearFields(); 
                    loginForm.Show();
                    break;
                }
            }
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (!isBackButtonClicked)
            {
                Application.Exit();
            }
        }

        private void repeat_password_tb_TextChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void email_tb_TextChanged(object sender, EventArgs e) { }
        private void password_tb_TextChanged(object sender, EventArgs e) { }
        private void log_tb_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e){}

    }
}
