using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AISHospitalPharmacy
{
    public partial class Verification : Form
    {
        public Verification()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == Form2.uLogin && textBox2.Text == Form2.uPassword)
            {
                Users f = new Users();
                f.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Учётная запись не подтверждена.\nПроверьте логин и пароль.");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f = new MainForm();
            f.Show();
            this.Hide();
        }
    }
}
