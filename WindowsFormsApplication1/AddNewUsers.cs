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
    public partial class AddNewUsers : Form
    {

        public AddNewUsers()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
            this.Hide();
        }
        private string GetPassword() {
            string abc = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#$%^&*()"; //набор символов
            Random rnd = new Random();
            int kol = rnd.Next(5, 20); ; // кол-во символов
            string result = null;

            int lng = abc.Length;
            for (int i = 0; i < kol; i++)
                result += abc[rnd.Next(lng)];
            return result;
            }
        private string GetLogin()
        {
            string abc = "1234567890qwertyuiopasdfghjklzxcvbnm"; //набор символов
            Random rnd = new Random();
            int kol = rnd.Next(3, 15); ; // кол-во символов
            string result = null;

            int lng = abc.Length;
            for (int i = 0; i < kol; i++)
                result += abc[rnd.Next(lng)];
            return result+"@com";
        }
        private void CleanFloders() {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            
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

                        string password = GetPassword();
                        string usr = null;
                        string login = GetLogin();

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
                        writer.WriteLine(userInfo);
                        MessageBox.Show("Пользователь успешно добавлен!");
                        CleanFloders();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

       
    }
}
