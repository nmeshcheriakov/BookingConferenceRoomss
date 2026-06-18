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
    public partial class AdminBookingForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;
        private DataTable bookingTable;
        private bool isBackButtonClicked = false;

        public AdminBookingForm()
        {
            InitializeComponent();
            LoadBookings();
            this.FormClosing += AdminBookingForm_FormClosing;
        }

        private void LoadBookings()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string query = @"SELECT 
                                b.[Id] as 'ID',
                                u.[login] as 'Пользователь',
                                cr.[Name] as 'Зал',
                                b.[CheckIn] as 'Начало',
                                b.[CheckOut] as 'Конец',
                                b.[Guests] as 'Гости',
                                s.[Name] as 'Статус',
                                b.[StatusId] as 'StatusId'
                            FROM [Booking] b
                            JOIN [User] u ON b.[UserId] = u.[Id]
                            JOIN [ConferenceRooms] cr ON b.[ConferenceRoomId] = cr.[Id]
                            JOIN [StatusBooking] s ON b.[StatusId] = s.[Id]
                            ORDER BY b.[Id] DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    bookingTable = new DataTable();
                    adapter.Fill(bookingTable);
                }

                dataGridViewBookings.Columns.Clear();

                //ID
                DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ID",
                    HeaderText = "ID",
                    Width = 50,
                    ReadOnly = true
                };
                dataGridViewBookings.Columns.Add(colId);

                //пользователь
                DataGridViewTextBoxColumn colUser = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Пользователь",
                    HeaderText = "Пользователь",
                    Width = 120,
                    ReadOnly = true
                };
                dataGridViewBookings.Columns.Add(colUser);

                //зал
                DataGridViewTextBoxColumn colRoom = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Зал",
                    HeaderText = "Зал",
                    Width = 120,
                    ReadOnly = true
                };
                dataGridViewBookings.Columns.Add(colRoom);

                //начало
                DataGridViewTextBoxColumn colStart = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Начало",
                    HeaderText = "Начало",
                    Width = 130,
                    ReadOnly = true
                };
                dataGridViewBookings.Columns.Add(colStart);

                //конец
                DataGridViewTextBoxColumn colEnd = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Конец",
                    HeaderText = "Конец",
                    Width = 130,
                    ReadOnly = true
                };
                dataGridViewBookings.Columns.Add(colEnd);

                //гости
                DataGridViewTextBoxColumn colGuests = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Гости",
                    HeaderText = "Гости",
                    Width = 60,
                    ReadOnly = true
                };
                dataGridViewBookings.Columns.Add(colGuests);

                //статус 
                DataGridViewComboBoxColumn colStatus = new DataGridViewComboBoxColumn
                {
                    DataPropertyName = "StatusId",
                    HeaderText = "Статус",
                    Width = 120,
                    DataSource = GetStatuses(),
                    DisplayMember = "Name",
                    ValueMember = "Id"
                };
                dataGridViewBookings.Columns.Add(colStatus);

                // Скрытая колонка для хранения StatusId
                DataGridViewTextBoxColumn colStatusId = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "StatusId",
                    HeaderText = "StatusId",
                    Visible = false
                };
                dataGridViewBookings.Columns.Add(colStatusId);

                dataGridViewBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewBookings.RowTemplate.Height = 40;
                dataGridViewBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewBookings.MultiSelect = false;
                dataGridViewBookings.DataSource = bookingTable;

                // Раскрашиваем строки по статусам
                dataGridViewBookings.CellFormatting += DataGridViewBookings_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки бронирований: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetStatuses()
        {
            DataTable statusTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT [Id], [Name] FROM [StatusBooking]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(statusTable);
            }
            return statusTable;
        }

        private void DataGridViewBookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.ColumnIndex == 6 && e.Value != null)
            {
                try
                {
                    int statusId = Convert.ToInt32(e.Value);
                    string statusName = GetStatusName(statusId);

                    if (!string.IsNullOrEmpty(statusName))
                    {
                        e.Value = statusName;
                        e.FormattingApplied = true;

                        DataGridViewRow row = dataGridViewBookings.Rows[e.RowIndex];
                        switch (statusId)
                        {
                            case 1: //активна
                                row.DefaultCellStyle.BackColor = Color.LightGreen;
                                break;
                            case 2: //отменена
                                row.DefaultCellStyle.BackColor = Color.LightCoral;
                                break;
                            case 3: //завершена
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                                break;
                            default:
                                row.DefaultCellStyle.BackColor = Color.White;
                                break;
                        }
                    }
                }
                catch { }
            }
        }

        private string GetStatusName(int statusId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT [Name] FROM [StatusBooking] WHERE [Id] = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", statusId);
                object result = cmd.ExecuteScalar();
                return result?.ToString() ?? "";
            }
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            // Проверка изменений
            bool hasChanges = false;
            foreach (DataRow row in bookingTable.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    hasChanges = true;
                    break;
                }
            }

            if (!hasChanges)
            {
                MessageBox.Show("Нет изменений для сохранения.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (DataRow row in bookingTable.Rows)
                    {
                        if (row.RowState == DataRowState.Modified)
                        {
                            int bookingId = Convert.ToInt32(row["ID"]);
                            int newStatusId = Convert.ToInt32(row["StatusId"]);

                            string update = "UPDATE [Booking] SET [StatusId] = @StatusId WHERE [Id] = @Id";
                            SqlCommand cmd = new SqlCommand(update, connection, transaction);
                            cmd.Parameters.AddWithValue("@StatusId", newStatusId);
                            cmd.Parameters.AddWithValue("@Id", bookingId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    bookingTable.AcceptChanges();
                    MessageBox.Show("Статусы бронирований обновлены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookings();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            isBackButtonClicked = true;
            this.Close();
        }

        private void AdminBookingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isBackButtonClicked)
            {
                Application.Exit();
            }

            
        }

        
    }
}