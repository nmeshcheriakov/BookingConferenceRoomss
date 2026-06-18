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
using BCrypt.Net;

namespace BookingConferenceRoomss
{
    public partial class AdminUserForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BookingDB"].ConnectionString;

        private bool isBackButtonClicked = false;

        private DataTable userTable;
        public AdminUserForm()
        {
            InitializeComponent();
            LoadUsers();
            this.FormClosing += AdminUserForm_FormClosing;
        }

        private void AdminUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            if (!isBackButtonClicked)
            {
                Application.Exit();
            }
        }

        private void AdminUserForm_Load(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT [Id], [login], [email], [password], [RoleId]
                                FROM [User] ORDER BY [ID]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    userTable = new DataTable();
                    adapter.Fill(userTable);
                }

                dataGridViewuser.Columns.Clear();


                DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 10,
                    ReadOnly = true
                };
                dataGridViewuser.Columns.Add(colId);

                DataGridViewTextBoxColumn collogin = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "login",
                    HeaderText = "Логин",
                    Width = 10
                };
                dataGridViewuser.Columns.Add(collogin);

                DataGridViewTextBoxColumn colemail = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "email",
                    HeaderText = "Почта",
                    Width = 10
                };
                dataGridViewuser.Columns.Add(colemail);

                DataGridViewTextBoxColumn colpassword = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "password",
                    HeaderText = "Пароль",
                    Width = 10
                };
                dataGridViewuser.Columns.Add(colpassword);

                DataGridViewTextBoxColumn colRoleId = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "RoleId",
                    HeaderText = "ID Роли",
                    Width = 10,
                };
                dataGridViewuser.Columns.Add(colRoleId);

                dataGridViewuser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewuser.RowTemplate.Height = 40;
                dataGridViewuser.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewuser.MultiSelect = false;


                dataGridViewuser.DataSource = userTable;



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void back_Click(object sender, EventArgs e)
        {
            isBackButtonClicked = true;
            this.Close();
        }

        private void adduser_bt_Click(object sender, EventArgs e)
        {
            DataRow newRow = userTable.NewRow();
            newRow["login"] = "";
            newRow["email"] = "";
            newRow["password"] = "";
            newRow["RoleId"] = 0;
            userTable.Rows.Add(newRow);
            dataGridViewuser.CurrentCell = dataGridViewuser.Rows[dataGridViewuser.Rows.Count - 1].Cells[1];
            dataGridViewuser.BeginEdit(true);
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in userTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if (string.IsNullOrWhiteSpace(row["login"].ToString()) || string.IsNullOrWhiteSpace(row["email"].ToString())
                        || string.IsNullOrWhiteSpace(row["password"].ToString()) || string.IsNullOrWhiteSpace(row["RoleId"].ToString()))
                    {
                        MessageBox.Show("Все данные пользователя должны быть заполнены!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (DataRow row in userTable.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            string plainPassword = row["password"].ToString().Trim();
                            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                            string maxIdQuery = "SELECT ISNULL(MAX([Id]), 0) + 1 FROM [User]";
                            SqlCommand cmd = new SqlCommand(maxIdQuery, connection, transaction);
                            int newId = (int)cmd.ExecuteScalar();
                            row["Id"] = newId;

                            string insert = @"INSERT INTO [User] ([Id], [login], [email], [password], [RoleId])
                                      VALUES (@Id, @login, @email, @password, @RoleId)";
                            cmd = new SqlCommand(insert, connection, transaction);
                            cmd.Parameters.AddWithValue("@Id", newId);
                            cmd.Parameters.AddWithValue("@login", row["login"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@email", row["email"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@password", hashedPassword); // Сохраняем хэш
                            cmd.Parameters.AddWithValue("@RoleId", Convert.ToInt32(row["RoleId"]));

                            cmd.ExecuteNonQuery();
                        }
                        else if (row.RowState == DataRowState.Modified)
                        {
                            // Обновление пользователя
                            string oldPassword = row["password", DataRowVersion.Original].ToString();
                            string newPassword = row["password"].ToString().Trim();

                            string finalPassword;

                            if (oldPassword != newPassword)
                            {
                                
                                finalPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                            }
                            else
                            {
                                
                                finalPassword = newPassword;
                            }

                            string update = @"UPDATE [User]
                                      SET [login] = @login, [email] = @email,
                                          [password] = @password, [RoleId] = @RoleId
                                      WHERE [Id] = @Id";
                            SqlCommand cmd = new SqlCommand(update, connection, transaction);
                            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(row["Id"]));
                            cmd.Parameters.AddWithValue("@login", row["login"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@email", row["email"].ToString().Trim());
                            cmd.Parameters.AddWithValue("@password", finalPassword);
                            cmd.Parameters.AddWithValue("@RoleId", Convert.ToInt32(row["RoleId"]));

                            cmd.ExecuteNonQuery();
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            // Удаление пользователя
                            int userId = Convert.ToInt32(row["Id", DataRowVersion.Original]);
                            //Удаляем брони пользователя
                            string deleteBookings = "DELETE FROM [Booking] WHERE [UserId] = @UserId";
                            SqlCommand cmd = new SqlCommand(deleteBookings, connection, transaction);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.ExecuteNonQuery();

                            string deleteUsers = "DELETE FROM [User] WHERE [Id] = @Id";
                            cmd = new SqlCommand(deleteUsers, connection, transaction);
                            cmd.Parameters.AddWithValue("@Id", userId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    userTable.AcceptChanges();
                    MessageBox.Show("Все изменения сохранены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteuser_bt_Click(object sender, EventArgs e)
        {
            if (dataGridViewuser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewuser.SelectedRows[0];
            string userName = selectedRow.Cells[1].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show(
                $"Удалить пользователя с логином «{userName}»?\nНажмите «Сохранить», чтобы подтвердить.",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dataGridViewuser.Rows.RemoveAt(selectedRow.Index);
            }
        }
    }
}

