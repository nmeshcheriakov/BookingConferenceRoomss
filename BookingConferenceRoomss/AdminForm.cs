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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookingConferenceRoomss
{
    public partial class AdminForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;

        private DataTable roomsTable;

        private bool isBackButtonClicked = false;

        public AdminForm()
        {
            InitializeComponent();
            LoadRooms();
            this.FormClosing += AdminForm_FormClosing;
            

        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (!isBackButtonClicked)
            {
                Application.Exit();
            }
        }


        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            isBackButtonClicked = true;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadRooms()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT [Id], [Name], [Capacity], [description], [image] 
                             FROM [ConferenceRooms] ORDER BY [Id]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    roomsTable = new DataTable();       
                    adapter.Fill(roomsTable);
                }

                dataGridView1.Columns.Clear();


                DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 50,
                    ReadOnly = true
                };
                dataGridView1.Columns.Add(colId);

                DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Name",
                    HeaderText = "Название",
                    Width = 120
                };
                dataGridView1.Columns.Add(colName);

                DataGridViewTextBoxColumn colCapacity = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Capacity",
                    HeaderText = "Вместимость",
                    Width = 80
                };
                dataGridView1.Columns.Add(colCapacity);

                DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "description",
                    HeaderText = "Описание",
                    Width = 150
                };
                dataGridView1.Columns.Add(colDesc);

                DataGridViewImageColumn colImage = new DataGridViewImageColumn
                {
                    DataPropertyName = "image",
                    Name = "image",
                    HeaderText = "Фото",
                    Width = 150,
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                dataGridView1.Columns.Add(colImage);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 150;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                
                dataGridView1.DataSource = roomsTable;

               
                LoadImagesIntoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки залов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }

        private void LoadImagesIntoGrid()
        {
            // Находим индекс колонки с фото по имени
            int imageColumnIndex = -1;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name == "image" || col.HeaderText == "Фото")
                {
                    imageColumnIndex = col.Index;
                    break;
                }
            }

            if (imageColumnIndex == -1) return; // Если колонка не найдена

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index < roomsTable.Rows.Count)
                {
                    DataRow dataRow = roomsTable.Rows[row.Index];
                    if (dataRow["image"] != DBNull.Value && dataRow["image"] != null)
                    {
                        byte[] imageBytes = (byte[])dataRow["image"];
                        if (imageBytes.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                Image original = Image.FromStream(ms);
                                // Используем найденный индекс
                                if (imageColumnIndex >= 0 && imageColumnIndex < row.Cells.Count)
                                {
                                    row.Cells[imageColumnIndex].Value = ResizeImage(original, 160, 100);
                                }
                            }
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 120;
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void changeimage_bt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите зал в таблице.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string roomName = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString() ?? "";

            // Проверка на активные бронирования
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = @"SELECT COUNT(*) FROM [Booking] 
                              WHERE [ConferenceRoomId] = @RoomId 
                              AND [StatusId] = 1";

                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@RoomId", roomId);
                int activeBookings = (int)checkCmd.ExecuteScalar();

                if (activeBookings > 0)
                {
                    MessageBox.Show($"Невозможно изменить фото зала «{roomName}».\n" +
                                    $"У зала есть {activeBookings} активных бронирований.\n" +
                                    "Сначала завершите или отмените все активные бронирования.",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите фото зала";
                openFileDialog.Filter = "Изображения (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);

                    // Обновляем в DataTable
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    roomsTable.Rows[rowIndex]["image"] = imageBytes;

                    // Обновляем в DataGridView
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image original = Image.FromStream(ms);
                        dataGridView1.Rows[rowIndex].Cells[dataGridView1.Columns.Count - 1].Value = ResizeImage(original, 160, 100);
                    }

                    // ===== ИСПРАВЛЕНИЕ: перезагружаем данные после сохранения =====
                    MessageBox.Show("Фото обновлено! Нажмите «Сохранить», чтобы применить изменения.",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void addroom_bt_Click(object sender, EventArgs e)
        {
            DataRow newRow = roomsTable.NewRow();
            newRow["Name"] = "Новый зал";
            newRow["Capacity"] = 0;
            newRow["description"] = "";
            newRow["image"] = DBNull.Value;
            roomsTable.Rows.Add(newRow);

            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
            dataGridView1.BeginEdit(true);
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            // Проверка заполнения полей
            foreach (DataRow row in roomsTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if (string.IsNullOrWhiteSpace(row["Name"].ToString()))
                    {
                        MessageBox.Show("Название зала не может быть пустым!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (row["Capacity"] == DBNull.Value || Convert.ToInt32(row["Capacity"]) <= 0)
                    {
                        MessageBox.Show("Вместимость зала должна быть больше 0!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataRow row in roomsTable.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        // Новый зал
                        string maxIdQuery = "SELECT ISNULL(MAX([Id]), 0) + 1 FROM [ConferenceRooms]";
                        SqlCommand cmd = new SqlCommand(maxIdQuery, connection);
                        int newId = (int)cmd.ExecuteScalar();
                        row["Id"] = newId;

                        string insert = @"INSERT INTO [ConferenceRooms] ([Id], [Name], [Capacity], [description], [image])
                                  VALUES (@Id, @Name, @Capacity, @Description, @Image)";
                        cmd = new SqlCommand(insert, connection);
                        cmd.Parameters.AddWithValue("@Id", newId);
                        cmd.Parameters.AddWithValue("@Name", row["Name"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Capacity", Convert.ToInt32(row["Capacity"]));

                        if (string.IsNullOrWhiteSpace(row["description"].ToString()))
                            cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Description", row["description"].ToString().Trim());

                        if (row["image"] == DBNull.Value || row["image"] == null)
                        {
                            cmd.Parameters.Add("@Image", SqlDbType.Image).Value = DBNull.Value;
                        }
                        else
                        {
                            byte[] imageBytes = (byte[])row["image"];
                            cmd.Parameters.Add("@Image", SqlDbType.Image).Value = imageBytes;
                        }

                        cmd.ExecuteNonQuery();
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        int roomId = Convert.ToInt32(row["Id"]);
                        string roomName = row["Name"].ToString().Trim();

                        // Получаем старые значения из базы
                        string getOriginalQuery = "SELECT [Name], [Capacity], [description], [image] FROM [ConferenceRooms] WHERE [Id] = @Id";
                        SqlCommand getOriginalCmd = new SqlCommand(getOriginalQuery, connection);
                        getOriginalCmd.Parameters.AddWithValue("@Id", roomId);

                        string oldName = "";
                        int oldCapacity = 0;
                        string oldDescription = "";
                        byte[] oldImage = null;

                        using (SqlDataReader reader = getOriginalCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oldName = reader["Name"].ToString().Trim();
                                oldCapacity = Convert.ToInt32(reader["Capacity"]);
                                oldDescription = reader["description"]?.ToString().Trim() ?? "";

                                if (reader["image"] != DBNull.Value && reader["image"] != null)
                                {
                                    oldImage = (byte[])reader["image"];
                                }
                            }
                        }

                        string newName = row["Name"].ToString().Trim();
                        int newCapacity = Convert.ToInt32(row["Capacity"]);
                        string newDescription = row["description"]?.ToString().Trim() ?? "";
                        byte[] newImage = null;

                        if (row["image"] != DBNull.Value && row["image"] != null)
                        {
                            newImage = (byte[])row["image"];
                        }

                        // ===== ИСПРАВЛЕНО: проверяем все изменения, включая фото =====
                        bool isDataChanged = (oldName != newName) ||
                                             (oldCapacity != newCapacity) ||
                                             (oldDescription != newDescription);

                        // Проверяем, изменилось ли фото
                        bool isImageChanged = false;
                        if (oldImage == null && newImage != null) isImageChanged = true;
                        else if (oldImage != null && newImage == null) isImageChanged = true;
                        else if (oldImage != null && newImage != null)
                        {
                            if (oldImage.Length != newImage.Length) isImageChanged = true;
                        }

                        // Если есть изменения - проверяем активные бронирования
                        if (isDataChanged || isImageChanged)
                        {
                            // Проверка активных бронирований (ТОЛЬКО если меняются данные)
                            if (isDataChanged)  // Проверяем только если меняются название/вместимость/описание
                            {
                                string checkQuery = "SELECT COUNT(*) FROM [Booking] WHERE [ConferenceRoomId] = @RoomId AND [StatusId] = 1";
                                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                                checkCmd.Parameters.AddWithValue("@RoomId", roomId);
                                int activeBookings = (int)checkCmd.ExecuteScalar();

                                if (activeBookings > 0)
                                {
                                    MessageBox.Show($"Невозможно изменить данные зала «{roomName}».\n" +
                                                    $"У зала есть {activeBookings} активных бронирований.\n" +
                                                    "Сначала завершите или отмените все активные бронирования.",
                                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Сохраняем изменения
                            string update = @"UPDATE [ConferenceRooms]
                                      SET [Name] = @Name, [Capacity] = @Capacity,
                                          [description] = @Description, [image] = @Image
                                      WHERE [Id] = @Id";
                            SqlCommand cmd = new SqlCommand(update, connection);
                            cmd.Parameters.AddWithValue("@Id", roomId);
                            cmd.Parameters.AddWithValue("@Name", newName);
                            cmd.Parameters.AddWithValue("@Capacity", newCapacity);

                            if (string.IsNullOrWhiteSpace(newDescription))
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@Description", newDescription);

                            if (newImage == null || newImage.Length == 0)
                            {
                                cmd.Parameters.Add("@Image", SqlDbType.Image).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@Image", SqlDbType.Image).Value = newImage;
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        // Удаление зала
                        int roomId = Convert.ToInt32(row["Id", DataRowVersion.Original]);

                        string checkQuery = "SELECT COUNT(*) FROM [Booking] WHERE [ConferenceRoomId] = @RoomId AND [StatusId] = 1";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                        checkCmd.Parameters.AddWithValue("@RoomId", roomId);
                        int activeBookings = (int)checkCmd.ExecuteScalar();

                        if (activeBookings > 0)
                        {
                            MessageBox.Show($"Невозможно удалить зал.\n" +
                                            $"У зала есть {activeBookings} активных бронирований.\n" +
                                            "Сначала завершите или отмените все активные бронирования.",
                                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string deleteBookings = "DELETE FROM [Booking] WHERE [ConferenceRoomId] = @Id";
                        SqlCommand cmd = new SqlCommand(deleteBookings, connection);
                        cmd.Parameters.AddWithValue("@Id", roomId);
                        cmd.ExecuteNonQuery();

                        string deleteRoom = "DELETE FROM [ConferenceRooms] WHERE [Id] = @Id";
                        cmd = new SqlCommand(deleteRoom, connection);
                        cmd.Parameters.AddWithValue("@Id", roomId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            roomsTable.AcceptChanges();
            MessageBox.Show("Все изменения сохранены!", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadRooms(); // Перезагружаем таблицу
        }

        private void deleteroom_bt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string roomName = selectedRow.Cells[1].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show(
                $"Удалить зал «{roomName}»?\nНажмите «Сохранить», чтобы подтвердить.",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(selectedRow.Index);
            }
        }
    }
}



    

