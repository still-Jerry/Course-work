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
    public partial class MainForm : Form
    {
        public static bool fl1 = false;
        public MainForm()
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
            find = new StreamReader("Медикаменты.txt", Encoding.GetEncoding(1251));
            //using (StreamReader find = File.OpenText("Сотрудники.txt"))
            using (find)
            {
                String input = null;
                Boolean ah = true;
                while ((input = find.ReadLine()) != null)
                {
                    string[] subs = input.Split(';');

                    if (ah)
                    {
                        for (int i = 0; i < subs.Length-1; i++)
                        {
                            dataGridViewPreparats.Columns.Add(subs[i], subs[i]);
                            dataGridViewPreparats.Font = new Font("Verbena", 8);
                            dataGridViewPreparats.Columns[i].Width = 70;
                            comboBox1.Items.Add(subs[i]);

                        }
                        comboBox1.SelectedIndex = 0;

                        ah = false;

                      
                    }
                    else {
                        dataGridViewPreparats.Rows.Add(subs);
                    
                    }

                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //ЗАЕБАШ СВИЧ КЕЙС!!!!!
            dataGridViewPreparats.ClearSelection();

            if (string.IsNullOrWhiteSpace(searchPreparats.Text))
                return;

            var values = searchPreparats.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridViewPreparats.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridViewPreparats.Rows[i];

                    if (row.Cells[0].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = Convert.ToString(dataGridViewPreparats[0, 1].Value);
            searchPreparats.Text = s+";";
            File.Delete("Сотрудники1.txt");

            using (StreamWriter writer = new StreamWriter("Медикаменты1.txt", true, Encoding.GetEncoding("windows-1251")))
            //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
            {
                try
                {
                    for (int j = 0; j < dataGridViewPreparats.Rows.Count-1; j++)
                    {
                        for (int i = 0; i < dataGridViewPreparats.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridViewPreparats.Rows[j].Cells[i].Value + ";");
                        }

                        writer.WriteLine();
                    }

                    MessageBox.Show("Файл успешно сохранен");
                }
                catch
                {
                    MessageBox.Show("Ошибка при сохранении файла!");
                }


            } 
        }

       
    }
}
