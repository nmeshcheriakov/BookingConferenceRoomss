namespace BookingConferenceRoomss
{
    partial class AdminForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addroom_bt = new System.Windows.Forms.Button();
            this.deleteroom_bt = new System.Windows.Forms.Button();
            this.save_bt = new System.Windows.Forms.Button();
            this.changeimage_bt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(771, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Управление конференц залами";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.back.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.back.Location = new System.Drawing.Point(12, 423);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(142, 33);
            this.back.TabIndex = 1;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(748, 336);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // addroom_bt
            // 
            this.addroom_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addroom_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addroom_bt.Location = new System.Drawing.Point(320, 423);
            this.addroom_bt.Name = "addroom_bt";
            this.addroom_bt.Size = new System.Drawing.Size(142, 33);
            this.addroom_bt.TabIndex = 3;
            this.addroom_bt.Text = "Добавить зал";
            this.addroom_bt.UseVisualStyleBackColor = true;
            this.addroom_bt.Click += new System.EventHandler(this.addroom_bt_Click);
            // 
            // deleteroom_bt
            // 
            this.deleteroom_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteroom_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteroom_bt.Location = new System.Drawing.Point(616, 423);
            this.deleteroom_bt.Name = "deleteroom_bt";
            this.deleteroom_bt.Size = new System.Drawing.Size(142, 33);
            this.deleteroom_bt.TabIndex = 4;
            this.deleteroom_bt.Text = "Удалить выбанный";
            this.deleteroom_bt.UseVisualStyleBackColor = true;
            this.deleteroom_bt.Click += new System.EventHandler(this.deleteroom_bt_Click);
            // 
            // save_bt
            // 
            this.save_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.save_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save_bt.Location = new System.Drawing.Point(468, 423);
            this.save_bt.Name = "save_bt";
            this.save_bt.Size = new System.Drawing.Size(142, 33);
            this.save_bt.TabIndex = 5;
            this.save_bt.Text = "Сохранить";
            this.save_bt.UseVisualStyleBackColor = true;
            this.save_bt.Click += new System.EventHandler(this.save_bt_Click);
            // 
            // changeimage_bt
            // 
            this.changeimage_bt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeimage_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeimage_bt.Location = new System.Drawing.Point(160, 423);
            this.changeimage_bt.Name = "changeimage_bt";
            this.changeimage_bt.Size = new System.Drawing.Size(156, 33);
            this.changeimage_bt.TabIndex = 6;
            this.changeimage_bt.Text = "Сменить фото";
            this.changeimage_bt.UseVisualStyleBackColor = true;
            this.changeimage_bt.Click += new System.EventHandler(this.changeimage_bt_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(771, 470);
            this.Controls.Add(this.changeimage_bt);
            this.Controls.Add(this.save_bt);
            this.Controls.Add(this.deleteroom_bt);
            this.Controls.Add(this.addroom_bt);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "AdminForm";
            this.Text = "Управление залами";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button addroom_bt;
        private System.Windows.Forms.Button deleteroom_bt;
        private System.Windows.Forms.Button save_bt;
        private System.Windows.Forms.Button changeimage_bt;
    }
}