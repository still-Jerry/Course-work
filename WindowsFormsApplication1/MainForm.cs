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
using Excel = Microsoft.Office.Interop.Excel;
namespace AISHospitalPharmacy
{
    public partial class MainForm : Form
    {
        public static Boolean chSavePrepavets = false;
        public static Boolean chSaveOrders = false;
        public static Boolean chSaveSup = false;
        public static Boolean chSaveBranch = false;
        public static Boolean chSaveDelay = false;

        public String path_PreparationsBranch = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public String path_PreparationsBranchArchive = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public String path_SupplierReport = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public String path_SupplierReportArchive = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public String path_Delay = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public String path_DelayArchive = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static bool fl1 = false;
        public MainForm()
        {
            InitializeComponent();
            label1.Text = Form2.UserName;
            if (Form2.usr == "администратор")
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
            Verification f = new Verification();
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
                        for (int i = 0; i < subs.Length; i++)
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
                            Double conclyse = Convert.ToInt32(subs[1]) * Convert.ToDouble(subs[2]);
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
            find = new StreamReader("Заказы на склад.txt", Encoding.GetEncoding(1251));
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
            chSavePrepavets = true;
            CopyPreparats.Enabled = true;
            //переменная разрешающая перезаписывать файл только при отсуствии некорректностей
            Boolean err = true;
            for (int j = 0; j < dataGridViewPreparats.Rows.Count - 1; j++)
            {
                for (int i = 0; i < dataGridViewPreparats.Rows[j].Cells.Count; i++)
                {
                    //проверка на пустую ячейку
                    if (dataGridViewPreparats.Rows[j].Cells[i].Value == null)
                    {
                        MessageBox.Show("Некоторые поля пусты" + j + "   " + i);
                        err = false;
                        break;
                    }
                    //провека на формат
                    if (i == 1)
                    {
                        try
                        {
                            Int32 symbol = Convert.ToInt32(dataGridViewPreparats.Rows[j].Cells[i].Value);
                        }
                        catch
                        {
                            MessageBox.Show("В столбцax 'кол-во' значения должны быть в  числовом формате");
                            err = false;
                            break;
                        }
                    }
                    if (i == 2)
                    {
                        try
                        {
                            Double symbol = Convert.ToDouble(dataGridViewPreparats.Rows[j].Cells[i].Value);
                        }
                        catch
                        {
                            MessageBox.Show("В столбце 'цена' значения должны быть в  числовом формате");
                            err = false;
                            break;
                        }
                    }
                    if (i == 3)
                    {
                        try
                        {
                            String Main = Convert.ToString(dataGridViewPreparats.Rows[j].Cells[i].Value);
                            String whithot = Main.Replace("д", "");
                            whithot = whithot.Replace("г", "");
                            whithot = whithot.Replace("м", "");
                            Int32 symbol = Convert.ToInt32(whithot);
                            if (Main.Replace(Convert.ToString(symbol), "") == "д" || Main.Replace(Convert.ToString(symbol), "") == "г" || Main.Replace(Convert.ToString(symbol), "") == "м")
                            {

                            }
                            else
                            {
                                MessageBox.Show("В столбце 'срок годности' значения должны быть в  числовом формате и содержать с себе одну из букв 'д,м,г'");
                                err = false;
                                break;
                            }
                            //if (coutD.Substring(coutD.Length - 1, coutD.Length) == "д" || coutD.Substring(coutD.Length - 1, coutD.Length) == "м" || coutD.Substring(coutD.Length - 1, coutD.Length) == "г")
                            //{
                            //}
                            //else {
                            //    MessageBox.Show("В столбце 'срок годности' значения должны быть в  числовом формате и содержать с себе одну из букв 'д,м,г'");
                            //    err = false;
                            //    break;
                            //}
                        }
                        catch
                        {
                            MessageBox.Show("В столбце 'срок годности' значения должны быть в  числовом формате и содержать с себе одну из букв 'д,м,г' "+j+i);
                            err = false;
                            break;
                        }
                    }
                    if (i == 4)
                    {
                        try
                        {
                            DateTime symbol = Convert.ToDateTime(dataGridViewPreparats.Rows[j].Cells[i].Value);
                            if (symbol > DateTime.Today) {
                                MessageBox.Show("Дата изготовления не может быть больше текущей даты");
                                err = false;
                                break;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("В столбце 'дата изготовления' значения должны быть в формате даты");
                            err = false;
                            break;
                        }
                    }
                }
            }
            if (err)
            {
                //удаляем старую версию фала, записываем новую из таблицы на экране
                //каждую ячейку добавляем к строке и ставим точку с запятой

                try
                {
                    File.Delete("Медикаменты склада.txt");
                    using (StreamWriter writer = new StreamWriter("Медикаменты склада.txt", true, Encoding.GetEncoding("windows-1251")))
                    {
                        for (int j = 0; j < dataGridViewPreparats.Rows.Count; j++)
                        {
                            //if (!err) { break; }
                            for (int i = 0; i < dataGridViewPreparats.Rows[j].Cells.Count; i++)
                            {

                                if (j == 0)
                                {
                                    if (i == dataGridViewPreparats.Rows[j].Cells.Count - 1)
                                    {
                                        writer.Write(dataGridViewPreparats.Columns[i].Name);
                                    }
                                    else
                                    {
                                        writer.Write(dataGridViewPreparats.Columns[i].Name + ";");
                                    }
                                }
                                else if (i == dataGridViewPreparats.Rows[j].Cells.Count - 1)
                                {
                                    writer.Write(dataGridViewPreparats.Rows[j - 1].Cells[i].Value);
                                }
                                else
                                {
                                    writer.Write(dataGridViewPreparats.Rows[j - 1].Cells[i].Value + ";");
                                }
                            }
                            //if (!err) { break; }

                            writer.WriteLine();
                        }
                        MessageBox.Show("Файл \"Медикаменты склада.txt\" успешно сохранен.");
                    }

                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Не найден файл для перезаписи.");
                }
                catch (Exception y)
                {
                    MessageBox.Show("Ошибка сохранения файла \"Медикаменты склада.txt\"\n" + y.ToString());
                }
            }
            //удаляем старую версию фала, записываем новую из таблицы на экране
            //каждую ячейку добавляем к строке и ставим точку с запятой
            //try
            //{
            //    File.Delete("Медикаменты склада.txt");
            //    using (StreamWriter writer = new StreamWriter("Медикаменты склада.txt", true, Encoding.GetEncoding("windows-1251")))
            //    {
            //        for (int j = 0; j < dataGridViewPreparats.Rows.Count; j++)
            //        {
            //            if (err)
            //            {
            //                for (int i = 0; i < dataGridViewPreparats.Rows[j].Cells.Count; i++)
            //                {
            //                    //провека на пустоту ячеек
            //                    if (dataGridViewPreparats.Rows[j].Cells[i].Value == null)
            //                    {
            //                        MessageBox.Show("Некоторые ячейки пустые");
            //                        err = false;
            //                        break;
            //                    }
            //                    //проверка сходимости форматов
            //                    if (i == 2 || i == 1)
            //                    {
            //                        try
            //                        {
            //                            Int32 symbol = Convert.ToInt32(dataGridViewPreparats.Rows[j].Cells[i].Value);
            //                        }
            //                        catch
            //                        {
            //                            MessageBox.Show("В столбцax 'цена' и 'кол-во' значения должны быть в  числовом формате");
            //                            err = false;
            //                            break;
            //                        }
            //                    }
            //                    if (i == 4)
            //                    {
            //                        try
            //                        {
            //                            DateTime symbol = Convert.ToDateTime(dataGridViewPreparats.Rows[j].Cells[i].Value);
            //                        }
            //                        catch
            //                        {
            //                            MessageBox.Show("В столбце 'дата изготовления' значения должны быть в формате даты");
            //                            err = false;
            //                            break;
            //                        }
            //                    }
            //                    //если всё норм перезаписываем файл
            //                    else
            //                    {
            //                        //    writer.Write(dataGridViewPreparats.Rows[j].Cells[i].Value + ";");
            //                        //    //запись шапки таблицы

            //                        if (j == 0)
            //                        {
            //                            if (i == dataGridViewPreparats.Rows[j].Cells.Count - 1)
            //                            {
            //                                writer.Write(dataGridViewPreparats.Columns[i].Name);
            //                            }
            //                            else
            //                            {
            //                                writer.Write(dataGridViewPreparats.Columns[i].Name + ";");
            //                            }
            //                        }
            //                        else if (i == dataGridViewPreparats.Rows[j].Cells.Count - 1)
            //                        {
            //                            writer.Write(dataGridViewPreparats.Rows[j - 1].Cells[i].Value);
            //                        }
            //                        else
            //                        {
            //                            writer.Write(dataGridViewPreparats.Rows[j - 1].Cells[i].Value + ";");
            //                        }
            //                    }
            //                }
            //                writer.WriteLine();
            //            }
            //            else { break; }
            //        }
            //        if (err)
            //        {
            //            MessageBox.Show("Файл \"Медикаменты склада.txt\" успешно сохранен");
            //        }
            //        else
            //        {
            //            MessageBox.Show("Проверьте правильность заполнения ячеек");
            //        }
            //    }
            //}
            //catch (FileNotFoundException)
            //{
            //    MessageBox.Show("Не найден файл для перезаписи.");
            //}
            //catch (Exception y)
            //{
            //    MessageBox.Show("Ошибка сохранения файла \"Медикаменты склада.txt\"\n" + y.ToString());
            //}



            //try{
            //  File.Delete("Медикаменты склада.txt");
            //  using (StreamWriter writer = new StreamWriter("Медикаменты склада.txt", true, Encoding.GetEncoding("windows-1251")))
            //  {
            //      for (int j = 0; j < dataGridViewPreparats.Rows.Count; j++)
            //      {
            //          for (int i = 0; i < dataGridViewPreparats.Rows[j].Cells.Count; i++)
            //          {
            //              //проверка на пустоту ячеек
            //              if (dataGridViewPreparats.Rows[j].Cells[i].Value == null && dataGridViewPreparats.Rows.Count!=j)
            //              {
            //                  MessageBox.Show("Некоторые ячейки пустые");
            //                  err = false;
            //                  break;
            //              }
            //              //проверка сходимости форматов
            //              else if (i == 2 || i == 1)
            //              {
            //                  try
            //                  {
            //                      Int32 symbol = Convert.ToInt32(dataGridViewPreparats.Rows[j].Cells[i].Value);
            //                  }
            //                  catch
            //                  {
            //                      MessageBox.Show("В столбцax 'цена' и 'кол-во' значения должны быть в  числовом формате");
            //                      err = false;
            //                      break;
            //                  }
            //              }
            //              else if (i == 4)
            //              {
            //                  try
            //                  {
            //                      DateTime symbol = Convert.ToDateTime(dataGridViewPreparats.Rows[j].Cells[i].Value);
            //                  }
            //                  catch
            //                  {
            //                      MessageBox.Show("В столбце 'дата изготовления' значения должны быть в формате даты");
            //                      err = false;
            //                      break;
            //                  }
            //              }
            //              //если всё норм перезаписываем файл
            //              else
            //              {
            //                  //    writer.Write(dataGridViewPreparats.Rows[j].Cells[i].Value + ";");
            //                  //    //запись шапки таблицы

            //                  if (j == 0)
            //                  {
            //                      if (i == dataGridViewPreparats.Rows[j].Cells.Count - 1)
            //                      {
            //                          writer.Write(dataGridViewPreparats.Columns[i].Name);
            //                      }
            //                      else
            //                      {
            //                          writer.Write(dataGridViewPreparats.Columns[i].Name + ";");
            //                      }
            //                  }
            //                  else if (i == dataGridViewPreparats.Rows[j].Cells.Count - 1)
            //                  {
            //                      writer.Write(dataGridViewPreparats.Rows[j - 1].Cells[i].Value);
            //                  }
            //                  else
            //                  {
            //                      writer.Write(dataGridViewPreparats.Rows[j - 1].Cells[i].Value + ";");
            //                  }
            //              }
            //          }
            //              if (!err){ break;}
            //              writer.WriteLine();
            //          }
            //      }
            //      if (err)
            //      {
            //          MessageBox.Show("Файл успешно сохранен");
            //      }
            //      else
            //      {
            //          MessageBox.Show("Проверьте правильность заполнения ячеек");
            //      }
            //}
            //catch (FileNotFoundException)
            //{
            //    MessageBox.Show("Не найден файл для перезаписи.");
            //}
            //catch (Exception y)
            //{
            //    MessageBox.Show("Ошибка сохранения файла \"Медикаменты склада.txt\"\n" + y.ToString());

            //}


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
            chSaveOrders = true;
            CopyOrders.Enabled = true;
            //переменная разрешающая перезаписывать файл только при отсуствии некорректностей
            Boolean err = true;
            for (int j = 0; j < dataGridViewOrders.Rows.Count - 1; j++)
            {
                for (int i = 0; i < dataGridViewOrders.Rows[j].Cells.Count; i++)
                {
                    //проверка на пустую ячейку
                    if (dataGridViewOrders.Rows[j].Cells[i].Value == null)
                    {
                        MessageBox.Show("Некоторые поля пусты");
                        err = false;
                        break;
                    }
                    //провека на формат
                    if (i == 1)
                    {
                        try
                        {
                            Int32 symbol = Convert.ToInt32(dataGridViewOrders.Rows[j].Cells[i].Value);
                        }
                        catch
                        {
                            MessageBox.Show("В столбце 'кол-во' значения должны быть в числовом формате");
                            err = false;
                            break;
                        }
                    }

                }
            }
            if (err)
            {
                //удаляем старую версию фала, записываем новую из таблицы на экране
                //каждую ячейку добавляем к строке и ставим точку с запятой
                try
                {
                    File.Delete("Заказы от отделений.txt");
                    using (StreamWriter writer = new StreamWriter("Заказы от отделений.txt", true, Encoding.GetEncoding("windows-1251")))
                    {
                        for (int j = 0; j < dataGridViewOrders.Rows.Count; j++)
                        {
                            for (int i = 0; i < dataGridViewOrders.Rows[j].Cells.Count; i++)
                            {
                                if (j == 0)
                                {
                                    if (i == dataGridViewOrders.Rows[j].Cells.Count - 1)
                                    {
                                        writer.Write(dataGridViewOrders.Columns[i].Name);
                                    }
                                    else
                                    {
                                        writer.Write(dataGridViewOrders.Columns[i].Name + ";");
                                    }
                                }
                                else if (i == dataGridViewOrders.Rows[j].Cells.Count - 1)
                                {
                                    writer.Write(dataGridViewOrders.Rows[j - 1].Cells[i].Value);
                                }
                                else
                                {
                                    writer.Write(dataGridViewOrders.Rows[j - 1].Cells[i].Value + ";");
                                }
                            }

                            writer.WriteLine();
                        }
                        MessageBox.Show("Файл \"Заказы от отделений.txt\" успешно сохранен.");
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Не найден файл для перезаписи.");
                }
                catch (Exception y)
                {
                    MessageBox.Show("Ошибка сохранения файла \"Заказы от отделений.txt\"\n" + y.ToString());
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
            ////ЗАЕБАШ СВИЧ КЕЙС!!!!!
            //dataGridSupplierReport.ClearSelection();

            //if (string.IsNullOrWhiteSpace(searchSupplierReport.Text))
            //    return;

            //var values = searchSupplierReport.Text.Split(new char[] { ' ' },
            //    StringSplitOptions.RemoveEmptyEntries);

            //for (int i = 0; i < dataGridSupplierReport.RowCount - 1; i++)
            //{
            //    foreach (string value in values)
            //    {
            //        var row = dataGridSupplierReport.Rows[i];
            //        int z = comboBox3.SelectedIndex;
            //        if (z == 1)
            //        {
            //            z = 0;
            //        }
            //        else if (z == 0)
            //        {
            //            z = 1;
            //        }
            //        if (row.Cells[z].Value.ToString().Contains(value))
            //        {
            //            row.Selected = true;
            //        }

            //    }
            //}
        }

        private void SaveReports_Click(object sender, EventArgs e)
        {
            chSaveDelay = true;
            button2.Enabled = true;
            if (checkBox3.Checked == true)
            {
                var res = MessageBox.Show(path_Delay + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path_Delay = path_dialog.SelectedPath;
                        };
                }
            }
            
            //String DocName = "\\Отчёт о просрочке" + "_" + DateTime.Today.ToShortDateString();

            try
            {
                Excel.Application excelapp = new Excel.Application();
                Excel.Workbook workbook = excelapp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                for (int i = 0; i < dataGridDelay.RowCount; i++)
                {
                    for (int j = 1; j < dataGridDelay.ColumnCount + 1; j++)
                    {
                        if (i == 0)
                        {
                            worksheet.Rows[i+1].Columns[j] = dataGridDelay.Columns[j - 1].HeaderText;
                            //worksheet.Cells.Borders.xlLineStyle.xlLineStyleSingle;
                            worksheet.Rows[i+1].Columns[j].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            //worksheet.AutoFilterMode = true;

                        }
                        else
                        {
                            worksheet.Rows[i+1].Columns[j] = dataGridDelay.Rows[i - 1].Cells[j - 1].Value;
                            worksheet.Rows[i+1].Columns[j].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        }
                        worksheet.Columns[i+1].AutoFit();
                    }
                }
                
                Excel.Range _excelCells1 = (Excel.Range)worksheet.get_Range("A" + (dataGridDelay.RowCount + 2), "E" + (dataGridDelay.RowCount + 6)).Cells;
                // Производим объединение
                _excelCells1.Merge(Type.Missing);
                worksheet.Cells[dataGridDelay.RowCount + 2, 1] = "OOO \"Медицина и Диагностика\". ИНН 5260372828\\526001001, 603005,\n" +
                                        "Нижегородская обл. Нижний Новгород г. Красная Слобода, дю 9,\n" +
                                        "кор. литер А, кв 40, р/с 407028108500008236 в Филиал N 6318 ВТБ24 (ЗАО)\n" +
                                        "г. Самара, БИК 043602955, кopp/c 30101810700000000955";
                worksheet.Rows[dataGridDelay.RowCount + 2].AutoFit();
                excelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(@path_Delay + "\\Отчёт о просрочке.xls");
                excelapp.Quit();
                MessageBox.Show("Файл " + @path_Delay +"\\Отчёт о просрочке.xls успешно сохранён");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка сохранения файла \"Отчёт о просрочке.xls\"\n" + y.ToString());
            
            }
        
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                var res = MessageBox.Show(path_DelayArchive + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path_DelayArchive = path_dialog.SelectedPath;
                        };
                }
            }
            try
            {
                File.Delete(@path_DelayArchive + "\\Отчёт о просрочке.zip");
                using (var fileStream = new FileStream(@path_DelayArchive+"\\Отчёт о просрочке.zip", FileMode.Create))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(@path_Delay+"\\Отчёт о просрочке.xls", "Отчёт о просрочке.xls");
                }
                MessageBox.Show("Файл \"Отчёт о просрочке.xls\" успешно архивирован.");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка при архивировании файла \"Отчёт о просрочке.xls\"\n" + y.ToString());
            }
        

        }

        private void CopyOrders_Click(object sender, EventArgs e)
        {
            
            try
            {
                var path=Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var res = MessageBox.Show(path + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path = path_dialog.SelectedPath;
                        };
                }
                File.Delete(path+"\\Заказы от отделений.zip");
                using (var fileStream = new FileStream(path+"\\Заказы от отделений.zip", FileMode.Create))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile("Заказы от отделений.txt", "Заказы от отделений.txt");
                }
                MessageBox.Show("Файл \"Заказы от отделений.txt\" успешно архивирован.");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка при архивировании файла \"Заказы от отделений.txt\"\n" + y.ToString());
            }
        }

        private void CopyPreparats_Click(object sender, EventArgs e)
        {
            
            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var res = MessageBox.Show(path + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path = path_dialog.SelectedPath;
                        };
                }
                File.Delete(path + "\\Медикаменты склада.zip");
                using (var fileStream = new FileStream(path + "\\Медикаменты склада.zip", FileMode.Create))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile("Медикаменты склада.txt", "Медикаменты склада.txt");
                }
                MessageBox.Show("Файл \"Медикаменты склада.txt\" успешно архивирован.");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка при архивировании файла \"Медикаменты склада.txt\"\n" + y.ToString());
            }
        }

     

        private void dataGridSupplierReport_VisibleChanged(object sender, EventArgs e)
        {
            dataGridSupplierReport.Rows.Clear();
            var find = new StreamReader("Заказы на склад.txt", Encoding.GetEncoding(1251));
            //using (StreamReader find = File.OpenText("Сотрудники.txt"))
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
                        ah = false;
                    }
                    else
                    {
                        string[] supply = { subs[1], subs[0], subs[2], subs[3], subs[4] };
                        dataGridSupplierReport.Rows.Add(supply);
                    }

                }
            

            }
        }

        private void findPreparationsBranch_Click(object sender, EventArgs e)
        {
            chSaveBranch = true;
            button3.Enabled = true;
            
            if (checkBox1.Checked == true)
            {
                var res = MessageBox.Show(path_PreparationsBranch + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path_PreparationsBranch = path_dialog.SelectedPath;
                        };
                }
            }
            
            //String DocName = "\\Отчёт о достатках_недостатках" + "_" + DateTime.Today.ToShortDateString();

            try
            {
                Excel.Application excelapp = new Excel.Application();
                Excel.Workbook workbook = excelapp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                for (int i = 0; i < dataGridPreparationsBranch.RowCount; i++)
                {
                    for (int j = 1; j < dataGridPreparationsBranch.ColumnCount + 1; j++)
                    {
                        if (i == 0)
                        {
                            worksheet.Rows[i + 1].Columns[j] = dataGridPreparationsBranch.Columns[j - 1].HeaderText;
                            //worksheet.Cells.Borders.xlLineStyle.xlLineStyleSingle;
                            worksheet.Rows[i + 1].Columns[j].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            //worksheet.AutoFilterMode = true;

                        }
                        else
                        {
                            worksheet.Rows[i + 1].Columns[j] = dataGridPreparationsBranch.Rows[i - 1].Cells[j - 1].Value;
                            worksheet.Rows[i + 1].Columns[j].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        }
                        worksheet.Columns[i + 1].AutoFit();
                    }
                }
                Excel.Range _excelCells1 = (Excel.Range)worksheet.get_Range("A" + (dataGridPreparationsBranch.RowCount + 2), "E" + (dataGridPreparationsBranch.RowCount + 6)).Cells;
                // Производим объединение
                _excelCells1.Merge(Type.Missing);
                worksheet.Cells[dataGridPreparationsBranch.RowCount + 2, 1] = "OOO \"Медицина и Диагностика\". ИНН 5260372828\\526001001, 603005,\n" +
                                        "Нижегородская обл. Нижний Новгород г. Красная Слобода, дю 9,\n" +
                                        "кор. литер А, кв 40, р/с 407028108500008236 в Филиал N 6318 ВТБ24 (ЗАО)\n" +
                                        "г. Самара, БИК 043602955, кopp/c 30101810700000000955";
                worksheet.Rows[dataGridPreparationsBranch.RowCount + 2].AutoFit();
                excelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(@path_PreparationsBranch + "\\Отчёт о достатках_недостатках.xls");
                excelapp.Quit();
                MessageBox.Show("Файл " + @path_PreparationsBranch +"\\Отчёт о достатках_недостатках.xls успешно сохранён");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка сохранения файла \"Отчёт о достатках_недостатках.xls\"\n" + y.ToString());
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                var res = MessageBox.Show(path_PreparationsBranchArchive + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path_PreparationsBranchArchive = path_dialog.SelectedPath;
                        };
                }
            }
            try
            {
                File.Delete(@path_PreparationsBranchArchive + "\\Отчёт о достатках_недостатках.zip");
                using (var fileStream = new FileStream(@path_PreparationsBranchArchive+"\\Отчёт о достатках_недостатках.zip", FileMode.Create))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(@path_PreparationsBranch+"\\Отчёт о достатках_недостатках.xls", "Отчёт о достатках_недостатках.xls");
                }
                MessageBox.Show("Файл \"Отчёт о достатках_недостатках.xls\" успешно архивирован.");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка при архивировании файла \"Отчёт о достатках_недостатках.xls\"\n" + y.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            chSaveSup = true;
            button4.Enabled = true;
            
            if (checkBox2.Checked == true)
            {
                var res = MessageBox.Show(path_SupplierReport + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path_SupplierReport = path_dialog.SelectedPath;
                        };
                }
            }
            
            //String DocName = "\\Отчёт о поставщиках" + "_" + DateTime.Today.ToShortDateString();

            try
            {
                Excel.Application excelapp = new Excel.Application();
                Excel.Workbook workbook = excelapp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                for (int i = 0; i < dataGridSupplierReport.RowCount; i++)
                {
                    for (int j = 1; j < dataGridSupplierReport.ColumnCount + 1; j++)
                    {
                        if (i == 0)
                        {
                            worksheet.Rows[i + 1].Columns[j] = dataGridSupplierReport.Columns[j - 1].HeaderText;
                            //worksheet.Cells.Borders.xlLineStyle.xlLineStyleSingle;
                            worksheet.Rows[i + 1].Columns[j].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            //worksheet.AutoFilterMode = true;

                        }
                        else
                        {
                            worksheet.Rows[i + 1].Columns[j] = dataGridSupplierReport.Rows[i - 1].Cells[j - 1].Value;
                            worksheet.Rows[i + 1].Columns[j].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        }
                        worksheet.Columns[i + 1].AutoFit();
                    }
                }
                Excel.Range _excelCells1 = (Excel.Range)worksheet.get_Range("A" + (dataGridSupplierReport.RowCount + 2), "E" + (dataGridSupplierReport.RowCount + 6)).Cells;
                // Производим объединение
                _excelCells1.Merge(Type.Missing);
                worksheet.Cells[dataGridSupplierReport.RowCount + 2, 1] = "OOO \"Медицина и Диагностика\". ИНН 5260372828\\526001001, 603005,\n" +
                                        "Нижегородская обл. Нижний Новгород г. Красная Слобода, дю 9,\n" +
                                        "кор. литер А, кв 40, р/с 407028108500008236 в Филиал N 6318 ВТБ24 (ЗАО)\n" +
                                        "г. Самара, БИК 043602955, кopp/c 30101810700000000955";
                worksheet.Rows[dataGridSupplierReport.RowCount + 2].AutoFit();
                excelapp.AlertBeforeOverwriting = false;
                workbook.SaveAs(@path_SupplierReport + "\\Отчёт о поставщиках.xls");
                excelapp.Quit();
                MessageBox.Show("Файл " + @path_SupplierReport +"\\Отчёт о поставщиках.xls успешно сохранён");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка сохранения файла \"Отчёт о поставщиках.xls\"\n" + y.ToString());
            
            }
        

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                var res = MessageBox.Show(path_SupplierReportArchive + "\nИзменить путь?", "Внимание", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {

                    using (var path_dialog = new FolderBrowserDialog())
                        if (path_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Путь к директории
                            path_SupplierReportArchive = path_dialog.SelectedPath;
                        };
                }
            }
            try
            {
                File.Delete(@path_SupplierReportArchive + "\\Отчёт о поставщиках.zip");
                using (var fileStream = new FileStream(@path_SupplierReportArchive+"\\Отчёт о поставщиках.zip", FileMode.Create))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(@path_SupplierReport+"\\Отчёт о поставщиках.xls", "Отчёт о поставщиках.xls");
                }
                MessageBox.Show("Файл \"Отчёт о поставщиках.xls\" успешно архивирован.");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка при архивировании файла \"Отчёт о поставщиках.xls\"\n" + y.ToString());
            }
        
        }

        private void dataGridPreparationsBranch_VisibleChanged(object sender, EventArgs e)
        {
            dataGridPreparationsBranch.Rows.Clear();
            var find = new StreamReader("Заказы от отделений.txt", Encoding.GetEncoding(1251));
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

                        string[] supply3 = { subs[2], subs[0], Convert.ToString(countS), subs[1], Convert.ToString(countS - Convert.ToInt32(subs[1])) };

                        dataGridPreparationsBranch.Rows.Add(supply3);
                    }

                }


            }
        }

        private void dataGridDelay_VisibleChanged(object sender, EventArgs e)
        {
            dataGridDelay.Rows.Clear();
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
                        ah = false;
                    }
                    else
                    {
                        DateTime dateTime = Convert.ToDateTime(subs[4]);
                        if (subs[3].Contains("д"))
                        {
                            string day = subs[3].Replace("д","");
                            dateTime=dateTime.AddDays(Convert.ToInt32(day));
                            //MessageBox.Show(Convert.ToString(dateTime));

                        }
                        else if (subs[3].Contains("м"))
                        {
                        //    string[] month = subs[3].Split('м');
                        //    dateTime.AddMonths(Convert.ToInt32(month[0]));
                            string month = subs[3].Replace("м", "");
                            dateTime=dateTime.AddMonths(Convert.ToInt32(month));
                            //MessageBox.Show(Convert.ToString(dateTime));
                        }
                        else if (subs[3].Contains("г"))
                        {
                            //string[] years = subs[3].Split('г');
                            //dateTime.AddYears(Convert.ToInt32(years[0]));
                            string years = subs[3].Replace("г", "");
                            dateTime=dateTime.AddYears(Convert.ToInt32(years));
                        }

                        if (dateTime < DateTime.Today)
                        {
                            //MessageBox.Show(Convert.ToString(dateTime) + Convert.ToString(DateTime.Today));
                            Double conclyse = Convert.ToInt32(subs[1]) * Convert.ToDouble(subs[2]);
                            string[] supply2 = { subs[0], subs[3], subs[4], subs[1], Convert.ToString(conclyse) };

                            dataGridDelay.Rows.Add(supply2);
                        }
                    }

                }

            }

        }

      

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (chSavePrepavets)
            {
                CopyPreparats.Enabled = true;
            }
            else
            {
                CopyPreparats.Enabled = false;
            }

            if (chSaveOrders)
            {
                CopyOrders.Enabled = true;
            }
            else
            {
                CopyOrders.Enabled = false;
            }
            if (chSaveDelay)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
            if (chSaveSup)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
            if (chSaveBranch)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }

        }


        
    }
}
