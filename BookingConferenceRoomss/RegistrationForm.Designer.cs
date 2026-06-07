namespace BookingConferenceRoomss
{
    partial class RegistrationForm
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
            this.log_tb = new System.Windows.Forms.TextBox();
            this.password_tb = new System.Windows.Forms.TextBox();
            this.email_tb = new System.Windows.Forms.TextBox();
            this.reg_bt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.repeat_password_tb = new System.Windows.Forms.TextBox();
            this.Back_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(243, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Регистрация";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // log_tb
            // 
            this.log_tb.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log_tb.Location = new System.Drawing.Point(253, 144);
            this.log_tb.Name = "log_tb";
            this.log_tb.Size = new System.Drawing.Size(269, 32);
            this.log_tb.TabIndex = 1;
            this.log_tb.TextChanged += new System.EventHandler(this.log_tb_TextChanged);
            // 
            // password_tb
            // 
            this.password_tb.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_tb.Location = new System.Drawing.Point(253, 298);
            this.password_tb.Name = "password_tb";
            this.password_tb.PasswordChar = '*';
            this.password_tb.Size = new System.Drawing.Size(269, 32);
            this.password_tb.TabIndex = 2;
            this.password_tb.TextChanged += new System.EventHandler(this.password_tb_TextChanged);
            // 
            // email_tb
            // 
            this.email_tb.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.email_tb.Location = new System.Drawing.Point(253, 223);
            this.email_tb.Name = "email_tb";
            this.email_tb.Size = new System.Drawing.Size(269, 32);
            this.email_tb.TabIndex = 3;
            this.email_tb.TextChanged += new System.EventHandler(this.email_tb_TextChanged);
            // 
            // reg_bt
            // 
            this.reg_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reg_bt.Location = new System.Drawing.Point(581, 421);
            this.reg_bt.Name = "reg_bt";
            this.reg_bt.Size = new System.Drawing.Size(175, 38);
            this.reg_bt.TabIndex = 4;
            this.reg_bt.Text = "Зарегистрироваться";
            this.reg_bt.UseVisualStyleBackColor = true;
            this.reg_bt.Click += new System.EventHandler(this.reg_bt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(249, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Логин";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(253, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "E-mail";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(253, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Пароль";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(253, 349);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Повторите пароль";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // repeat_password_tb
            // 
            this.repeat_password_tb.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repeat_password_tb.Location = new System.Drawing.Point(253, 377);
            this.repeat_password_tb.Name = "repeat_password_tb";
            this.repeat_password_tb.PasswordChar = '*';
            this.repeat_password_tb.Size = new System.Drawing.Size(269, 32);
            this.repeat_password_tb.TabIndex = 9;
            this.repeat_password_tb.TextChanged += new System.EventHandler(this.repeat_password_tb_TextChanged);
            // 
            // Back_bt
            // 
            this.Back_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Back_bt.Location = new System.Drawing.Point(65, 428);
            this.Back_bt.Name = "Back_bt";
            this.Back_bt.Size = new System.Drawing.Size(71, 31);
            this.Back_bt.TabIndex = 10;
            this.Back_bt.Text = "Назад";
            this.Back_bt.UseVisualStyleBackColor = true;
            this.Back_bt.Click += new System.EventHandler(this.Back_bt_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(799, 486);
            this.Controls.Add(this.Back_bt);
            this.Controls.Add(this.repeat_password_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reg_bt);
            this.Controls.Add(this.email_tb);
            this.Controls.Add(this.password_tb);
            this.Controls.Add(this.log_tb);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "RegistrationForm";
            this.Text = "Регистрация";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegistrationForm_FormClosed);
            this.Load += new System.EventHandler(this.RegistrationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox log_tb;
        private System.Windows.Forms.TextBox password_tb;
        private System.Windows.Forms.TextBox email_tb;
        private System.Windows.Forms.Button reg_bt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox repeat_password_tb;
        private System.Windows.Forms.Button Back_bt;
    }
}