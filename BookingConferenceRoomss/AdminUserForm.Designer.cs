namespace BookingConferenceRoomss
{
    partial class AdminUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.Button();
            this.dataGridViewuser = new System.Windows.Forms.DataGridView();
            this.adduser_bt = new System.Windows.Forms.Button();
            this.deleteuser_bt = new System.Windows.Forms.Button();
            this.save_bt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewuser)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Управление пользователями";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.back.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.back.Location = new System.Drawing.Point(22, 417);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(142, 33);
            this.back.TabIndex = 9;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // dataGridViewuser
            // 
            this.dataGridViewuser.AllowUserToAddRows = false;
            this.dataGridViewuser.AllowUserToDeleteRows = false;
            this.dataGridViewuser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewuser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewuser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewuser.Location = new System.Drawing.Point(22, 75);
            this.dataGridViewuser.MultiSelect = false;
            this.dataGridViewuser.Name = "dataGridViewuser";
            this.dataGridViewuser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewuser.Size = new System.Drawing.Size(748, 336);
            this.dataGridViewuser.TabIndex = 10;
            // 
            // adduser_bt
            // 
            this.adduser_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.adduser_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adduser_bt.Location = new System.Drawing.Point(170, 417);
            this.adduser_bt.Name = "adduser_bt";
            this.adduser_bt.Size = new System.Drawing.Size(142, 33);
            this.adduser_bt.TabIndex = 11;
            this.adduser_bt.Text = "Добавить ";
            this.adduser_bt.UseVisualStyleBackColor = true;
            this.adduser_bt.Click += new System.EventHandler(this.adduser_bt_Click);
            // 
            // deleteuser_bt
            // 
            this.deleteuser_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteuser_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteuser_bt.Location = new System.Drawing.Point(466, 417);
            this.deleteuser_bt.Name = "deleteuser_bt";
            this.deleteuser_bt.Size = new System.Drawing.Size(142, 33);
            this.deleteuser_bt.TabIndex = 12;
            this.deleteuser_bt.Text = "Удалить ";
            this.deleteuser_bt.UseVisualStyleBackColor = true;
            this.deleteuser_bt.Click += new System.EventHandler(this.deleteuser_bt_Click);
            // 
            // save_bt
            // 
            this.save_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.save_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save_bt.Location = new System.Drawing.Point(318, 417);
            this.save_bt.Name = "save_bt";
            this.save_bt.Size = new System.Drawing.Size(142, 33);
            this.save_bt.TabIndex = 13;
            this.save_bt.Text = "Сохранить";
            this.save_bt.UseVisualStyleBackColor = true;
            this.save_bt.Click += new System.EventHandler(this.save_bt_Click);
            // 
            // AdminUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.save_bt);
            this.Controls.Add(this.deleteuser_bt);
            this.Controls.Add(this.adduser_bt);
            this.Controls.Add(this.dataGridViewuser);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "AdminUserForm";
            this.Text = "Управление пользователями";
            this.Load += new System.EventHandler(this.AdminUserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewuser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.DataGridView dataGridViewuser;
        private System.Windows.Forms.Button adduser_bt;
        private System.Windows.Forms.Button deleteuser_bt;
        private System.Windows.Forms.Button save_bt;
    }
}