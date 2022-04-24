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

namespace AISHospitalPharmacy
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
                        for (int i = 0; i < subs.Length; i++)
                        {
                            dataGridViewWorkers.Columns.Add(subs[i], subs[i]);
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
            //переменная разрешающая перезаписывать файл только при отсуствии некорректностей
            Boolean err = true;
            for (int j = 0; j < dataGridViewWorkers.Rows.Count - 1; j++)
            {
                for (int i = 0; i < dataGridViewWorkers.Rows[j].Cells.Count; i++)
                {
                    //проверка на пустую ячейку
                    if (dataGridViewWorkers.Rows[j].Cells[i].Value == null)
                    {
                        MessageBox.Show("Некоторые поля пусты");
                        err = false;
                        break;
                    }
                    //провека на формат
                    if (i == 5)
                    {
                        try
                        {
                            Double symbol = Convert.ToDouble(dataGridViewWorkers.Rows[j].Cells[i].Value);
                        }
                        catch
                        {
                            MessageBox.Show("В столбце 'номер телефона' значения должны быть в числовом формате");
                            err = false;
                            break;
                        }
                    }
                    if (i == 3 || i==2)
                    {
                        try
                        {
                            DateTime symbol = Convert.ToDateTime(dataGridViewWorkers.Rows[j].Cells[i].Value);
                        }
                        catch
                        {
                            MessageBox.Show("В столбцах 'дата найма' и 'дата рождения' значения должны быть в формате даты" );
                            err = false;
                            break;
                        }
                    }
                    if (i == 9)
                    {
                        if (Convert.ToString(dataGridViewWorkers.Rows[j].Cells[i].Value) == "администратор" || Convert.ToString(dataGridViewWorkers.Rows[j].Cells[i].Value) == "пользователь") { continue; }
                       else{
                           MessageBox.Show("В столбце 'права' значение может быть только 'пользователь' или 'администратор'" + j + "   " + i);
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
                    File.Delete("Сотрудники.txt");
                    using (StreamWriter writer = new StreamWriter("Сотрудники.txt", true, Encoding.GetEncoding("windows-1251")))
                    {
                        for (int j = 0; j < dataGridViewWorkers.Rows.Count; j++)
                        {
                            for (int i = 0; i < dataGridViewWorkers.Rows[j].Cells.Count; i++)
                            {
                                if (j == 0)
                                {
                                    if (i == dataGridViewWorkers.Rows[j].Cells.Count - 1)
                                    {
                                        writer.Write(dataGridViewWorkers.Columns[i].Name);
                                    }
                                    else
                                    {
                                        writer.Write(dataGridViewWorkers.Columns[i].Name + ";");
                                    }
                                }
                                else if (i == dataGridViewWorkers.Rows[j].Cells.Count - 1)
                                {
                                    writer.Write(dataGridViewWorkers.Rows[j - 1].Cells[i].Value);
                                }
                                else
                                {
                                    writer.Write(dataGridViewWorkers.Rows[j - 1].Cells[i].Value + ";");
                                }
                            }

                            writer.WriteLine();
                        }
                        MessageBox.Show("Файл \"Сотрудники.txt\" успешно сохранен.");
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Не найден файл для перезаписи.");
                }
                catch (Exception y)
                {
                    MessageBox.Show("Ошибка сохранения файла \"Сотрудники.txt\"\n" + y.ToString());
                }
            
            } 
        }

        private void CopyWorkers_Click(object sender, EventArgs e)
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
                File.Delete(path + "\\Сотрудники.zip");
                using (var fileStream = new FileStream(path + "\\Сотрудники.zip", FileMode.Create))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile("Сотрудники.txt", "Сотрудники.txt");
                }
                MessageBox.Show("Файл \"Сотрудники.txt\" успешно архивирован.");

            }
            catch (Exception y)
            {
                MessageBox.Show("Ошибка при архивировании файла \"Сотрудники.txt\"\n" + y.ToString());
            }
        }

        
    }
}
