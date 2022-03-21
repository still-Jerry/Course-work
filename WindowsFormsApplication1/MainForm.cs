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
using System.IO.Compression;

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
            else
            {
                button10.Hide();
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            Users f = new Users();
            f.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var find = new StreamReader("Медикаменты склада.txt", Encoding.GetEncoding(1251));
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
                        for (int i = 0; i < subs.Length - 1; i++)
                        {
                            dataGridViewPreparats.Columns.Add(subs[i], subs[i]);
                            //dataGridViewPreparats.Font = new Font("Verbena", 8);
                            dataGridViewPreparats.Columns[i].Width = 100;
                            comboBox1.Items.Add(subs[i]);


                        }
                        comboBox1.SelectedIndex = 0;

                        ah = false;


                    }
                    else
                    {
                        dataGridViewPreparats.Rows.Add(subs);
                        DateTime dateTime = Convert.ToDateTime(subs[4]);
                        if (subs[3].Contains("д"))
                        {
                            string[] day = subs[3].Split('д');
                            dateTime.AddDays(Convert.ToInt32(day[0]));
                        }
                        else if (subs[3].Contains("м"))
                        {
                            string[] month = subs[3].Split('м');
                            dateTime.AddMonths(Convert.ToInt32(month[0]));
                        }
                        else if (subs[3].Contains("г"))
                        {
                            string[] years = subs[3].Split('г');
                            dateTime.AddYears(Convert.ToInt32(years[0]));
                        }

                        if (dateTime < DateTime.Today)
                        {
                            int conclyse = Convert.ToInt32(subs[1]) * Convert.ToInt32(subs[2]);
                            string[] supply2 = { subs[0], subs[3], subs[4], subs[1], Convert.ToString(conclyse) };

                            dataGridDelay.Rows.Add(supply2);
                        }
                    }

                }

            }
            find = new StreamReader("Заказы от отделений.txt", Encoding.GetEncoding(1251));
            //using (StreamReader find = File.OpenText("Сотрудники.txt"))
            using (find)
            {
                String input = null;
                Boolean ah = true;
                while ((input = find.ReadLine()) != null)
                {
                    string[] subs = input.Split(';');
                    //int conclyse = Convert.ToInt32(subs[3]) + Convert.ToInt32(subs[2]);
                    if (ah)
                    {
                        //for (int i = 0; i < 7; i++)
                        //{
                        //    //dataGridSupplierReport.Font = new Font("Verbena", 8);
                        //    //dataGridSupplierReport.Columns[i].Width = 90;


                        //}
                        for (int i = 0; i < subs.Length; i++)
                        {
                            dataGridViewOrders.Columns.Add(subs[i], subs[i]);
                            //dataGridViewOrders.Font = new Font("Verbena", 11);
                            //dataGridViewOrders.Columns[i].Width = 200;
                            comboBox2.Items.Add(subs[i]);


                        }
                        comboBox2.SelectedIndex = 0;

                        //for (int i = 0; i < 5; i++)
                        //{
                        //    dataGridPreparationsBranch.Font = new Font("Verbena", 8);
                        //    dataGridPreparationsBranch.Columns[i].Width = 100;
                        //    dataGridDelay.Font = new Font("Verbena", 8);
                        //    dataGridDelay.Columns[i].Width = 100;
                        //}
                        ah = false;
                    }
                    else
                    {
                        int countS = 0;
                        for (int j = 0; j < dataGridViewPreparats.Rows.Count - 1; j++)
                        {
                            String nameP = Convert.ToString(dataGridViewPreparats.Rows[j].Cells[0].Value);
                            if (subs[0] == nameP)
                            {
                                countS = Convert.ToInt32(dataGridViewPreparats.Rows[j].Cells[1].Value);
                            }
                        }
                        //string[] supply = { subs[3], subs[1], subs[2], subs[6], subs[5], subs[4], Convert.ToString(conclyse) };

                        //dataGridSupplierReport.Rows.Add(supply);

                        string[] supply3 = { subs[2], subs[0], Convert.ToString(countS), subs[1], Convert.ToString(countS - Convert.ToInt32(subs[1])) };

                        dataGridPreparationsBranch.Rows.Add(supply3);
                        dataGridViewOrders.Rows.Add(subs);
                        //dataGridViewOrders.Font = new Font("Verbena", 11);
                    }

                }


            }
            find = new StreamReader("Заказы на склад1.txt", Encoding.GetEncoding(1251));
            //using (StreamReader find = File.OpenText("Сотрудники.txt"))
            using (find)
            {
                String input = null;
                Boolean ah = true;
                while ((input = find.ReadLine()) != null)
                {
                    string[] subs = input.Split(';');
                    //int conclyse = Convert.ToInt32(subs[3]) + Convert.ToInt32(subs[2]);
                    if (ah)
                    {
                        for (int i = 0; i < subs.Length; i++)
                        {
                            //dataGridSupplierReport.Columns.Add(subs[i], subs[i]);
                            comboBox3.Items.Add(subs[i]);

                        }
                        comboBox3.SelectedIndex = 0;

                        ah = false;
                    }
                    else
                    {
                        string[] supply = { subs[1], subs[0], subs[2], subs[3], subs[4] };
                        dataGridSupplierReport.Rows.Add(supply);
                        dataGridSupplierReport.Rows.Add(subs);
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
                    int z = comboBox1.SelectedIndex;
                    if (row.Cells[z].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //foreach (GridViewRow dr in dataGridViewPreparats.Rows)
            //{
            //    if (dr.Cells[4].Text == "&nbsp;")// проверяем 4-й столбец на пустые ячейки
            //    {
            //        dr.Cells[4].BackColor = System.Drawing.Color.Yellow; // заодно покрасим

            //    }
            //}
            //for (int i = 0; i < dataGridViewPreparats.ColumnCount; i++)
            //{
            //    for (int j = 0; j < dataGridViewPreparats.RowCount; j++)
            //    {
            //        if (dataGridViewPreparats[i, j].Value = null)
            //        {
            //            Cells[4].BackColor = System.Drawing.Color.Yellow;
            //        }
            //    }
            //}
            string s = Convert.ToString(dataGridViewPreparats[0, 1].Value);
            searchPreparats.Text = s + ";";
            File.Delete("Медикаменты склада1.txt");
            Boolean err = true;
            using (StreamWriter writer = new StreamWriter("Медикаменты склада1.txt", true, Encoding.GetEncoding("windows-1251")))
            //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
            { 
                try
                {
                   
                    for (int j = 0; j < dataGridViewPreparats.Rows.Count - 1; j++)
                    {
                        if (err)
                        {
                            for (int i = 0; i < dataGridViewPreparats.Rows[j].Cells.Count; i++)
                            {
                                if (dataGridViewPreparats.Rows[j].Cells[i].Value == null)
                                {
                                    //    dataGridViewPreparats.Rows[j].Cells[i].Style = System.Drawing.Color.Yellow;
                                    MessageBox.Show("Некоторые ячейки пустые");
                                    err = false;
                                    break;
                                }
                                if (i == 2 || i == 1) {
                                    try
                                    {
                                        Int32 symbol = Convert.ToInt32(dataGridViewPreparats.Rows[j].Cells[i].Value);
                                    }
                                    catch {
                                        MessageBox.Show("В столбцax 'цена' и 'кол-во' значения должны быть в  числовом формате");
                                        err = false;
                                        break;
                                    }
                                }
                                if (i == 4)
                                {
                                    try
                                    {
                                        DateTime symbol = Convert.ToDateTime(dataGridViewPreparats.Rows[j].Cells[i].Value);
                                    }
                                    catch
                                    {
                                        MessageBox.Show("В столбце 'дата изготовления' значения должны быть в формате даты");
                                        err = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    writer.Write(dataGridViewPreparats.Rows[j].Cells[i].Value + ";");
                                }
                            }

                            writer.WriteLine();
                        }
                        else { break; }
                    }
                    if (err)
                    {
                        MessageBox.Show("Файл успешно сохранен");
                    }
                    else {
                        MessageBox.Show("Проверьте правильность заполнения ячеек");
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при сохранении файла!");
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ЗАЕБАШ СВИЧ КЕЙС!!!!!
            dataGridViewOrders.ClearSelection();

            if (string.IsNullOrWhiteSpace(searchOrders.Text))
                return;

            var values = searchOrders.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridViewOrders.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridViewOrders.Rows[i];
                    int z = comboBox1.SelectedIndex;
                    if (row.Cells[z].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }

                }
            }

        }

        private void SaveOrders_Click(object sender, EventArgs e)
        {
            string s = Convert.ToString(dataGridViewOrders[0, 1].Value);
            searchOrders.Text = s + ";";
            File.Delete("Заказы от отделений1.txt");

            using (StreamWriter writer = new StreamWriter("Заказы от отделений1.txt", true, Encoding.GetEncoding("windows-1251")))
            //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
            {
                try
                {
                    for (int j = 0; j < dataGridViewOrders.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i < dataGridViewOrders.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridViewOrders.Rows[j].Cells[i].Value + ";");
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

        private void button1_Click(object sender, EventArgs e)
        {
            ConsignmentNote f = new ConsignmentNote();
            f.Show();
            this.Hide();
        }

        private void findSupplierReport_Click(object sender, EventArgs e)
        {
            //ЗАЕБАШ СВИЧ КЕЙС!!!!!
            dataGridSupplierReport.ClearSelection();

            if (string.IsNullOrWhiteSpace(searchSupplierReport.Text))
                return;

            var values = searchSupplierReport.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataGridSupplierReport.RowCount - 1; i++)
            {
                foreach (string value in values)
                {
                    var row = dataGridSupplierReport.Rows[i];
                    int z = comboBox3.SelectedIndex;
                    if (z == 1)
                    {
                        z = 0;
                    }
                    else if (z == 0)
                    {
                        z = 1;
                    }
                    if (row.Cells[z].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }

                }
            }
        }

        private void SaveReports_Click(object sender, EventArgs e)
        {
            File.Delete("Отчёты");

            Directory.CreateDirectory("Отчёты");
            string s = Convert.ToString(dataGridViewOrders[0, 1].Value);
            searchOrders.Text = s + ";";
            File.Delete("Отчёт о поставщиках.txt");
            File.Delete("Отчёт о просрочке.txt");

            File.Delete("Отчёт о достатках_недостатках.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter("Отчёты\\Отчёт о поставщиках.txt", true, Encoding.GetEncoding("windows-1251")))
                //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
                {
                    writer.WriteLine("OOO \"Медицина и Диагностика\". ИНН 5260372828\\526001001, 603005," +
                                    "Нижегородская обл. Нижний Новгород г. Красная Слобода, дю 9," +
                                    "кор. литер А, кв 40, р/с 407028108500008236 в Филиал N 6318 ВТБ24 (ЗАО)" +
                                    "г. Самара, БИК 043602955, кopp/c 30101810700000000955");

                    for (int j = 0; j < dataGridSupplierReport.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i < dataGridSupplierReport.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridSupplierReport.Rows[j].Cells[i].Value + ";");
                        }

                        writer.WriteLine();
                    }

                    MessageBox.Show("Файл 'Отчёт о поставщиках' успешно сохранен");
                }
                using (StreamWriter writer = new StreamWriter("Отчёты\\Отчёт о просрочке.txt", true, Encoding.GetEncoding("windows-1251")))
                //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
                {
                    writer.WriteLine("OOO \"Медицина и Диагностика\". ИНН 5260372828\\526001001, 603005," +
                                    "Нижегородская обл. Нижний Новгород г. Красная Слобода, дю 9," +
                                    "кор. литер А, кв 40, р/с 407028108500008236 в Филиал N 6318 ВТБ24 (ЗАО)" +
                                    "г. Самара, БИК 043602955, кopp/c 30101810700000000955");

                    for (int j = 0; j < dataGridDelay.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i < dataGridDelay.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridDelay.Rows[j].Cells[i].Value + ";");
                        }

                        writer.WriteLine();
                    }

                    MessageBox.Show("Файл 'Отчёт о просрочке' успешно сохранен");
                }
                using (StreamWriter writer = new StreamWriter("Отчёты\\Отчёт о достатках_недостатках.txt", true, Encoding.GetEncoding("windows-1251")))
                //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
                {
                    writer.WriteLine("OOO \"Медицина и Диагностика\". ИНН 5260372828\\526001001, 603005," +
                                    "Нижегородская обл. Нижний Новгород г. Красная Слобода, дю 9," +
                                    "кор. литер А, кв 40, р/с 407028108500008236 в Филиал N 6318 ВТБ24 (ЗАО)" +
                                    "г. Самара, БИК 043602955, кopp/c 30101810700000000955");

                    for (int j = 0; j < dataGridPreparationsBranch.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i < dataGridPreparationsBranch.Rows[j].Cells.Count; i++)
                        {
                            writer.Write(dataGridPreparationsBranch.Rows[j].Cells[i].Value + ";");
                        }

                        writer.WriteLine();
                    }

                    MessageBox.Show("Файл 'Отчёт о достатках_недостатках' успешно сохранен");
                }

            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }




        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                File.Delete("Отчёты.zip");
                ZipFile.CreateFromDirectory("Отчёты", "Отчёты.zip");
                MessageBox.Show("Файлы успешно архивированы!");

            }
            catch
            {
                MessageBox.Show("Ошибка при архивировании файла!");
            }
        }
     

    }
}
