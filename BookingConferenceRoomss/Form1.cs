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


namespace BookingConferenceRoomss
{
    public partial class Form1 : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void entrance_tb_Click(object sender, EventArgs e)
        {
            string login = log_tb.Text.Trim();
            string password = password_tb.Text.Trim();
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Так нельзя!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос: ищем пользователя с таким логином и паролем и берем его RoleId
                    string loginQuery = "SELECT RoleId FROM [User] WHERE [Login] = @login AND [Password] = @password";

                    using (SqlCommand cmd = new SqlCommand(loginQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Выполняем запрос. ExecuteScalar возвращает одно конкретное значение (первую колонку первой строки)
                        object result = cmd.ExecuteScalar();

                        if (result != null) // Если нашли пользователя
                        {
                            int roleId = Convert.ToInt32(result);

                            if (roleId == 1)
                            {
                                MessageBox.Show("Добро пожаловать, Администратор!", "Вход успешен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Тут в будущем откроем форму админа:
                                // AdminForm adminForm = new AdminForm();
                                // adminForm.Show();
                                // this.Hide();
                            }
                            else if (roleId == 2)
                            {
                                MessageBox.Show("Добро пожаловать, Пользователь!", "Вход успешен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Тут в будущем откроем форму обычного юзера:
                                // UserForm userForm = new UserForm();
                                // userForm.Show();
                                // this.Hide();
                            }
                        }
                        else // Если result == null, значит, такого логина/пароля нет в базе
                        {
                            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Если что-то пошло не так с самой базой (например, не создалась)
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Создаем экземпляр формы регистрации
            RegistrationForm regForm = new RegistrationForm();

            // 2. Показываем форму регистрации
            regForm.Show();

            // 3. Прячем текущую форму авторизации (Form1), чтобы она не висела на заднем плане
            this.Hide();
        }
    }
}
