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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f = new MainForm();
            f.Show();
            this.Hide();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            var find = new StreamReader("Сотрудники.txt", Encoding.GetEncoding(1251));

            using (find)
            {
                String input = null;
                Boolean ah = true;
                while ((input = find.ReadLine()) != null)
                {
                    string[] subs = input.Split(';');

                    if (ah)
                    {
                        for (int i = 0; i < subs.Length - 2; i++)
                        {
                            dataGridViewWorkers.Columns.Add(subs[i], subs[i]);
                            dataGridViewWorkers.Font = new Font("Verbena", 8);
                            dataGridViewWorkers.Columns[i].Width = 90;
                            comboBox2.Items.Add(subs[i]);

                        }
                        comboBox2.SelectedIndex = 0;

                        ah = false;

                    }
                    else
                    {
                        dataGridViewWorkers.Rows.Add(subs);

                    }

                }

            }
        }

        private void FindWorkers_Click(object sender, EventArgs e)
        {
            dataGridViewWorkers.ClearSelection();

            if (string.IsNullOrWhiteSpace(searchWorkers.Text))
                return;

            var values = searchWorkers.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridViewWorkers.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridViewWorkers.Rows[i];
                    int z = comboBox2.SelectedIndex;
                    if (row.Cells[z].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }

                }
            }
        }

        private void SaveWorkers_Click(object sender, EventArgs e)
        {
            string s = Convert.ToString(dataGridViewWorkers[0, 1].Value);
            searchWorkers.Text = s + ";";
            File.Delete("Сотрудники1.txt");

            using (StreamWriter writer = new StreamWriter("Сотрудники1.txt", true, Encoding.GetEncoding("windows-1251")))
            //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
            {
                try
                {
                    for (int j = 0; j < dataGridViewWorkers.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i < dataGridViewWorkers.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridViewWorkers.Rows[j].Cells[i].Value + ";");
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
