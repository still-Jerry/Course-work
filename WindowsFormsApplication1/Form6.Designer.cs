﻿namespace WindowsFormsApplication1
{
    partial class Form6
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "\t\tРЕКВИЗИТЫ АПТЕКИ",
            "",
            "\t\tТОВАРНАЯ НАКЛАДНАЯ",
            "",
            "ЗАКАЗ №\t\t\tДАТА СОСТАВЛЕНИЯ",
            "",
            "№ по  \tкод ; \tнаим \tкол-во; постав получ; цена  цена ",
            "порядку; товара\tтовара;\t\t\t    за уп; общ",
            "",
            "",
            "",
            "ИТОГО:",
            "",
            "",
            "Заказ оформил: "});
            this.listBox1.Location = new System.Drawing.Point(13, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(319, 329);
            this.listBox1.TabIndex = 0;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 371);
            this.Controls.Add(this.listBox1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
    }
}