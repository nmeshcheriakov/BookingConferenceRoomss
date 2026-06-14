namespace BookingConferenceRoomss
{
    partial class MainFrom
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.guest = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.finish = new System.Windows.Forms.DateTimePicker();
            this.start = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.show_bt = new System.Windows.Forms.Button();
            this.search_tb = new System.Windows.Forms.TextBox();
            this.bron_bt = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.back = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guest)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.guest);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.finish);
            this.groupBox1.Controls.Add(this.start);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(-1, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // guest
            // 
            this.guest.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.guest.Location = new System.Drawing.Point(241, 58);
            this.guest.Name = "guest";
            this.guest.Size = new System.Drawing.Size(120, 32);
            this.guest.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(237, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Кол-во гостей";
            // 
            // finish
            // 
            this.finish.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.finish.Location = new System.Drawing.Point(6, 124);
            this.finish.Name = "finish";
            this.finish.Size = new System.Drawing.Size(200, 32);
            this.finish.TabIndex = 4;
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start.Location = new System.Drawing.Point(6, 58);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(200, 32);
            this.start.TabIndex = 3;
            this.start.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата и время окончания";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата и время начала";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.show_bt);
            this.groupBox2.Controls.Add(this.search_tb);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(399, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 144);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поиск";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // show_bt
            // 
            this.show_bt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.show_bt.Location = new System.Drawing.Point(256, 106);
            this.show_bt.Name = "show_bt";
            this.show_bt.Size = new System.Drawing.Size(126, 32);
            this.show_bt.TabIndex = 7;
            this.show_bt.Text = "Показать";
            this.show_bt.UseVisualStyleBackColor = true;
            this.show_bt.Click += new System.EventHandler(this.show_bt_Click);
            // 
            // search_tb
            // 
            this.search_tb.Location = new System.Drawing.Point(6, 30);
            this.search_tb.Name = "search_tb";
            this.search_tb.Size = new System.Drawing.Size(263, 32);
            this.search_tb.TabIndex = 6;
            this.search_tb.TextChanged += new System.EventHandler(this.search_tb_TextChanged);
            // 
            // bron_bt
            // 
            this.bron_bt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bron_bt.Location = new System.Drawing.Point(405, 154);
            this.bron_bt.Name = "bron_bt";
            this.bron_bt.Size = new System.Drawing.Size(376, 34);
            this.bron_bt.TabIndex = 8;
            this.bron_bt.Text = "Забронировать";
            this.bron_bt.UseVisualStyleBackColor = true;
            this.bron_bt.Click += new System.EventHandler(this.bron_bt_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(-1, 194);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(796, 252);
            this.dgv.TabIndex = 9;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // back
            // 
            this.back.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.back.Location = new System.Drawing.Point(-1, 452);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(142, 33);
            this.back.TabIndex = 10;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.back);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.bron_bt);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainFrom";
            this.Text = "MainFrom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guest)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker start;
        private System.Windows.Forms.DateTimePicker finish;
        private System.Windows.Forms.NumericUpDown guest;
        private System.Windows.Forms.TextBox search_tb;
        private System.Windows.Forms.Button show_bt;
        private System.Windows.Forms.Button bron_bt;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button back;
    }
}