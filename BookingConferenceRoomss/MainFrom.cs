using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookingConferenceRoomss
{
    public partial class MainFrom : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;
        private bool isBackButtonClicked = false;


        public int CurrentUserId { get; set; }

        public MainFrom()
        {
            InitializeComponent();
            this.FormClosing += MainFrom_FormClosing;
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            start.Format = DateTimePickerFormat.Custom;
            start.CustomFormat = "dd.MM.yyyy HH:mm";

            finish.Format = DateTimePickerFormat.Custom;
            finish.CustomFormat = "dd.MM.yyyy HH:mm";

            // Устанавливаем дефолтное время: текущее и +2 часа на встречу
            start.Value = DateTime.Now;
            finish.Value = DateTime.Now.AddHours(2);

            // Настройка счетчика гостей
            guest.Minimum = 1;
            guest.Value = 1;

            LoadAvailableRooms();

        }
        private void show_bt_Click(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }
        private void LoadAvailableRooms()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT [Id] as 'Номер зала', 
                                            [Name] as 'Название зала', 
                                            [Capacity] as 'Вместимость (чел.)', 
                                            [description] as 'Описание',
                                            [image]
                                     FROM [ConferenceRooms] CR
                                     WHERE [Capacity] >= @guests
                                     AND NOT EXISTS(
                                        SELECT 1 FROM [Booking] B
                                        WHERE B.[ConferenceRoomId] = CR.[Id]
                                        AND B.[StatusId] = 1 -- Проверяем только активные брони
                                        AND B.[CheckIn] < @finish
                                        AND B.[CheckOut] > @start
                                     )";

                    if (!string.IsNullOrWhiteSpace(search_tb.Text))
                    {
                        query += " AND ([Name] LIKE @search OR [description] LIKE @search)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@start", start.Value);
                        cmd.Parameters.AddWithValue("@finish", finish.Value);
                        cmd.Parameters.AddWithValue("@guests", (int)guest.Value);

                        if (!string.IsNullOrWhiteSpace(search_tb.Text))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + search_tb.Text.Trim() + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgv.DataSource = dt;

                        if (dgv.Columns["image"] is DataGridViewImageColumn imageCol)
                        {
                            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            imageCol.HeaderText = "Фото зала";
                        }

                        dgv.RowTemplate.Height = 110;
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            row.Height = 110;
                        }

                        dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отображения залов: {ex.Message}", "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bron_bt_Click(object sender, EventArgs e)
        {

            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Пожалуйста, выберите конференц-зал из списка таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (start.Value >= finish.Value)
            {
                MessageBox.Show("Время начала бронирования не может быть позже или равно времени окончания!", "Ошибка времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int roomId = Convert.ToInt32(dgv.CurrentRow.Cells["Номер зала"].Value);
            int guestsCount = (int)guest.Value;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand idCmd = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM [Booking]", connection);
                    int newBookingId = (int)idCmd.ExecuteScalar();

                    string insertQuery = @"INSERT INTO [Booking] ([Id], [CheckIn], [CheckOut], [Guests], [UserId], [ConferenceRoomId], [StatusId]) 
                                           VALUES (@id, @checkIn, @checkOut, @guests, @userId, @roomId, 1)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@id", newBookingId);
                        insertCmd.Parameters.AddWithValue("@checkIn", start.Value);
                        insertCmd.Parameters.AddWithValue("@checkOut", finish.Value);
                        insertCmd.Parameters.AddWithValue("@guests", guestsCount);
                        insertCmd.Parameters.AddWithValue("@userId", CurrentUserId);
                        insertCmd.Parameters.AddWithValue("@roomId", roomId);

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Зал успешно забронирован на ваше имя!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAvailableRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось сохранить бронирование: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void search_tb_TextChanged(object sender, EventArgs e)
        {
            LoadAvailableRooms();
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isBackButtonClicked)
            {
                Application.Exit();
            }
            else
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
        }

        private void back_Click(object sender, EventArgs e)
        {
            isBackButtonClicked = true;
            this.Close();
        }
    }
}
