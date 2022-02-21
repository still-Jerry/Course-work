using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            Form7.fl1 = true;
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Проверьте заполненность ВСЕХ полей");
                }
                else
                {

                    using (StreamWriter writer = new StreamWriter("Сотрудники.txt", true, Encoding.GetEncoding("windows-1251")))
                    //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
                    {
                        String[] subsw = null;
                        subsw = Form7.userInfo.Split(';');
                        string login = subsw[6];
                        string password = subsw[7];
                        string usr = subsw[8];

                        if (checkBox1.Checked == true)
                        {
                            usr = "администатор";
                        }
                        else
                        {
                            usr = "пользователь";

                        }

                        string userInfo = textBox1.Text + ';' + comboBox2.Text + ';' + dateTimePicker2.Text + ';' +
                           dateTimePicker1.Text + ';' + textBox2.Text + ';' + textBox3.Text + ';' + comboBox1.Text + ';' +
                           login + ';' + password + ';' + usr;
                        //writer.WriteLine(userInfo);
                        Form7.userInfo = userInfo;
                        MessageBox.Show("Пользователь успешно обновлён!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void Form8_Load(object sender, EventArgs e)
        {
            String[] subsw = null;
            subsw = Form7.userInfo.Split(';');

            textBox1.Text = subsw[0];
            comboBox2.Text = subsw[1];
            dateTimePicker2.Text = subsw[2];
            dateTimePicker1.Text = subsw[3];
            textBox2.Text = subsw[4];
            comboBox1.Text = subsw[5];
            string login = subsw[6];
            string password = subsw[7];
            string usr = subsw[8];
            if (usr == "администатор")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;

            }
        }
    }
}
