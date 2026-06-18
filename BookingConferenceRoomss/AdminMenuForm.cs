using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingConferenceRoomss
{
    public partial class AdminMenuForm : Form
    {

        private bool isBackButtonClicked = false;

        public AdminMenuForm()
        {
            InitializeComponent();
            this.FormClosing += AdminMenuForm_FormClosing;
        }

        private void AdminMenuForm_Load(object sender, EventArgs e)
        {
            
        }


        private void Back_Click(object sender, EventArgs e)
        {

            isBackButtonClicked = true;
            this.Close(); 
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rooms_bt_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.FormClosed += (s, args) => this.Show(); 
            adminForm.Show();
            this.Hide(); 
        }

        private void users_bt_Click(object sender, EventArgs e)
        {
            AdminUserForm adminUserForm = new AdminUserForm();
            adminUserForm.FormClosed += (s, args) => this.Show(); 
            adminUserForm.Show();
            this.Hide(); 
        }
        private void AdminMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если закрыли не через кнопку "Назад" - закрываем всё приложение
            if (!isBackButtonClicked)
            {
                Application.Exit();
            }
            else
            {
                // Если через кнопку "Назад" - показываем форму авторизации
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

        private void bookings_bt_Click(object sender, EventArgs e)
        {
            AdminBookingForm bookingForm = new AdminBookingForm();
            bookingForm.FormClosed += (s, args) => this.Show();
            bookingForm.Show();
            this.Hide();
        }
    }
}
