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

                    
                    string loginQuery = "SELECT RoleId FROM [User] WHERE [Login] = @login AND [Password] = @password";

                    using (SqlCommand cmd = new SqlCommand(loginQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", log_tb.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", password_tb.Text.Trim());

                        
                        object result = cmd.ExecuteScalar();

                        if (result != null) 
                        {
                            int roleId = Convert.ToInt32(result);

                            if (roleId == 1)
                            {
                                MessageBox.Show("Добро пожаловать, Администратор!", "Вход успешен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AdminMenuForm adminmenuForm = new AdminMenuForm();
                                
                                adminmenuForm.ShowDialog();
                                this.Hide();
                            }
                            else if (roleId == 2)
                            {
                                MessageBox.Show("Добро пожаловать!", "Вход успешен", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MainFrom userForm = new MainFrom();
                                userForm.FormClosed += (s, args) => this.Close(); 
                                userForm.Show();
                                this.Hide();

                            }
                        }
                        else 
                        {
                            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            RegistrationForm regForm = new RegistrationForm();
            regForm.Show();
            this.Hide();
        }
        
    }
}
