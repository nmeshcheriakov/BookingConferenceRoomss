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
        public AdminMenuForm()
        {
            InitializeComponent();
        }

        private void AdminMenuForm_Load(object sender, EventArgs e)
        {
            
        }


        private void Back_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rooms_bt_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            //adminForm.FormClosed += (s, args) => this.Close();
            this.Hide();
        }

        private void users_bt_Click(object sender, EventArgs e)
        {
            AdminUserForm adminUserForm = new AdminUserForm();
            adminUserForm.Show();
            //adminUserForm.FormClosed += (s, args) => this.Close();
            this.Hide();
        }
    }
}
