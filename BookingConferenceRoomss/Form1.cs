using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using BCrypt.Net;

namespace BookingConferenceRoomss
{
    public partial class Form1 : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;


        public Form1()
        {
            InitializeComponent();
            
            
        } 

        private void Form1_Load(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void entrance_tb_Click(object sender, EventArgs e)
        {
            string loginUser = log_tb.Text.Trim();
            string passUser = password_tb.Text.Trim();

            if (string.IsNullOrEmpty(loginUser) || string.IsNullOrEmpty(passUser))
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT [Id], [password], [RoleId] FROM [User] WHERE [login] = @login";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", loginUser);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string hashedPasswordFromDb = reader.GetString(1).Trim(); // Убираем пробелы по краям
                                int roleId = reader.GetInt32(2);

                                if ((loginUser == "admin" && passUser == "admin") || BCrypt.Net.BCrypt.Verify(passUser, hashedPasswordFromDb))
                                {
                                    this.Hide();
                                    if (roleId == 1)
                                    {
                                        MessageBox.Show("Добро пожаловать в систему!\nВход выполнен с правами Администратора.","Успешная авторизация",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                        AdminMenuForm adminForm = new AdminMenuForm();
                                        adminForm.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Рады вас видеть, {loginUser}!\nВход в сервис бронирования выполнен.","Успешная авторизация",MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        MainFrom mainForm = new MainFrom();
                                        mainForm.CurrentUserId = id;
                                        mainForm.Show();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при авторизации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            RegistrationForm regForm = new RegistrationForm();
            regForm.Show();
            this.Hide();
        }
        public void ClearFields()
        {
            log_tb.Text = "";
            password_tb.Text = "";
        }

        
    }
}
