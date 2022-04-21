namespace AISHospitalPharmacy
{
    partial class Users
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Users));
            this.CopyWorkers = new System.Windows.Forms.Button();
            this.FindWorkers = new System.Windows.Forms.Button();
            this.dataGridViewWorkers = new System.Windows.Forms.DataGridView();
            this.searchWorkers = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SaveWorkers = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkers)).BeginInit();
            this.SuspendLayout();
            // 
            // CopyWorkers
            // 
            this.CopyWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyWorkers.Location = new System.Drawing.Point(16, 390);
            this.CopyWorkers.Name = "CopyWorkers";
            this.CopyWorkers.Size = new System.Drawing.Size(218, 47);
            this.CopyWorkers.TabIndex = 44;
            this.CopyWorkers.Text = "Копировать базу";
            this.CopyWorkers.UseVisualStyleBackColor = true;
            // 
            // FindWorkers
            // 
            this.FindWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindWorkers.Location = new System.Drawing.Point(629, 44);
            this.FindWorkers.Name = "FindWorkers";
            this.FindWorkers.Size = new System.Drawing.Size(132, 30);
            this.FindWorkers.TabIndex = 43;
            this.FindWorkers.Text = "Поиск";
            this.FindWorkers.UseVisualStyleBackColor = true;
            this.FindWorkers.Click += new System.EventHandler(this.FindWorkers_Click);
            // 
            // dataGridViewWorkers
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewWorkers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewWorkers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWorkers.Location = new System.Drawing.Point(17, 80);
            this.dataGridViewWorkers.Name = "dataGridViewWorkers";
            this.dataGridViewWorkers.Size = new System.Drawing.Size(744, 304);
            this.dataGridViewWorkers.TabIndex = 42;
            // 
            // searchWorkers
            // 
            this.searchWorkers.Location = new System.Drawing.Point(16, 43);
            this.searchWorkers.Name = "searchWorkers";
            this.searchWorkers.Size = new System.Drawing.Size(452, 26);
            this.searchWorkers.TabIndex = 41;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(475, 43);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(147, 26);
            this.comboBox2.TabIndex = 40;
            // 
            // SaveWorkers
            // 
            this.SaveWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveWorkers.Location = new System.Drawing.Point(542, 390);
            this.SaveWorkers.Name = "SaveWorkers";
            this.SaveWorkers.Size = new System.Drawing.Size(218, 47);
            this.SaveWorkers.TabIndex = 39;
            this.SaveWorkers.Text = "Сохранить изменения";
            this.SaveWorkers.UseVisualStyleBackColor = true;
            this.SaveWorkers.Click += new System.EventHandler(this.SaveWorkers_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(629, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 32);
            this.button1.TabIndex = 45;
            this.button1.Text = "Вернуться";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RosyBrown;
            this.ClientSize = new System.Drawing.Size(777, 446);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CopyWorkers);
            this.Controls.Add(this.FindWorkers);
            this.Controls.Add(this.dataGridViewWorkers);
            this.Controls.Add(this.searchWorkers);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.SaveWorkers);
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сотрудники";
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CopyWorkers;
        private System.Windows.Forms.Button FindWorkers;
        private System.Windows.Forms.DataGridView dataGridViewWorkers;
        private System.Windows.Forms.TextBox searchWorkers;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button SaveWorkers;
        private System.Windows.Forms.Button button1;
    }
}