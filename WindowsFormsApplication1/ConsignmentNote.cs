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
    public partial class ConsignmentNote : Form
    {
        public ConsignmentNote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            MainForm f = new MainForm();
            f.Show();
            this.Hide();
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Int32 Sum = 0;
            for (int j = 0; j < dataGridConsignmentNote.Rows.Count - 1; j++)
            {
                dataGridConsignmentNote.Rows[j].Cells[dataGridConsignmentNote.Rows[j].Cells.Count - 1].Value = Convert.ToInt32(dataGridConsignmentNote.Rows[j].Cells[2].Value) * Convert.ToInt32(dataGridConsignmentNote.Rows[j].Cells[3].Value);
                Sum += Convert.ToInt32(dataGridConsignmentNote.Rows[j].Cells[dataGridConsignmentNote.Rows[j].Cells.Count - 1].Value);
            }
            label3.Text = Convert.ToString(Sum) + " p.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string s = Convert.ToString(dataGridConsignmentNote[0, 0].Value);
            //searchOrders.Text = s + ";";
            File.Delete("Заказы на склад1.txt");

            using (StreamWriter writer = new StreamWriter("Заказы на склад1.txt", true, Encoding.GetEncoding("windows-1251")))
            //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
            {
                try
                {
                    writer.WriteLine("Название препарата;Поставщик;Кол-во;Цена закупочная;Цена общая");

                    for (int j = 0; j < dataGridConsignmentNote.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i < dataGridConsignmentNote.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridConsignmentNote.Rows[j].Cells[i].Value + ";");
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
