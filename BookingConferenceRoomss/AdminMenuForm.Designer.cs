namespace BookingConferenceRoomss
{
    partial class AdminMenuForm
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
            this.rooms_bt = new System.Windows.Forms.Button();
            this.users_bt = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(91, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите раздел:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rooms_bt
            // 
            this.rooms_bt.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rooms_bt.Location = new System.Drawing.Point(5, 74);
            this.rooms_bt.Name = "rooms_bt";
            this.rooms_bt.Size = new System.Drawing.Size(213, 25);
            this.rooms_bt.TabIndex = 1;
            this.rooms_bt.Text = "Управление залами";
            this.rooms_bt.UseVisualStyleBackColor = true;
            this.rooms_bt.Click += new System.EventHandler(this.rooms_bt_Click);
            // 
            // users_bt
            // 
            this.users_bt.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.users_bt.Location = new System.Drawing.Point(5, 106);
            this.users_bt.Name = "users_bt";
            this.users_bt.Size = new System.Drawing.Size(213, 25);
            this.users_bt.TabIndex = 2;
            this.users_bt.Text = "Управление поьзователями";
            this.users_bt.UseVisualStyleBackColor = true;
            this.users_bt.Click += new System.EventHandler(this.users_bt_Click);
            // 
            // Back
            // 
            this.Back.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Back.Location = new System.Drawing.Point(5, 137);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(213, 25);
            this.Back.TabIndex = 3;
            this.Back.Text = "Выйти";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // AdminMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(334, 195);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.users_bt);
            this.Controls.Add(this.rooms_bt);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню администратора";
            this.Load += new System.EventHandler(this.AdminMenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rooms_bt;
        private System.Windows.Forms.Button users_bt;
        private System.Windows.Forms.Button Back;
    }
}