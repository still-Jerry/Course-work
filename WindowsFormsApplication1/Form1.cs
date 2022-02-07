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
    public partial class Form1 : Form
    {
        public static bool fl1 = false;
        public Form1()
        {
            InitializeComponent();
            label1.Text = Form2.UserName;
            if (Form2.usr == "администатор")
            {
                button10.Show();
            }
            else {
                button10.Hide();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1)
            {
                button5.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var find = new StreamReader("Заказы.txt", Encoding.GetEncoding(1251));
            //using (StreamReader find = File.OpenText("Сотрудники.txt"))
            using (find)
            {
                String input = null;
               
                while ((input = find.ReadLine()) != null)
                {
                    String inputNew = null;
                    for (int i = 0; i < input.Length; i++)
                    {

                        if (input[i] == ';')
                        {
                            inputNew+='\t';
                        }
                        else {
                            inputNew += input[i];
                        }
                    }
                    listBox2.Items.Add(inputNew);

                }

            }
        }
    }
}
