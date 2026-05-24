using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingConferenceRoomss
{
    public partial class AdminForm : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadAllBookings();
        }

        private void back_Click(object sender, EventArgs e)
        {
            AdminMenuForm adminMenuForm = new AdminMenuForm();
            adminMenuForm.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadAllBookings()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Красивый SQL-запрос, объединяющий таблицы для вывода читаемых данных админу
                    string query = @"SELECT b.[Id] as 'ID Брони', 
                                            u.[login] as 'Пользователь', 
                                            r.[Name] as 'Конференц-зал', 
                                            b.[CheckIn] as 'Начало', 
                                            b.[CheckOut] as 'Окончание', 
                                            b.[Guests] as 'Гостей', 
                                            s.[Name] as 'Статус'
                                     FROM [Booking] b
                                     JOIN [User] u ON b.[UserId] = u.[Id]
                                     JOIN [ConferenceRooms] r ON b.[ConferenceRoomId] = r.[Id]
                                     JOIN [StatusBooking] s ON b.[StatusId] = s.[Id]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dataGridView1 != null)
                    {
                       dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки бронирований: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
