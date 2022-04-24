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
using Word = Microsoft.Office.Interop.Word;
namespace AISHospitalPharmacy
{
    public partial class ConsignmentNote : Form
    {
        public  String path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
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
            Double Sum = 0;
            for (int j = 0; j < dataGridConsignmentNote.Rows.Count - 1; j++)
            {
                try
                {
                    dataGridConsignmentNote.Rows[j].Cells[dataGridConsignmentNote.Rows[j].Cells.Count - 1].Value = Convert.ToInt32(dataGridConsignmentNote.Rows[j].Cells[2].Value) * Convert.ToDouble(dataGridConsignmentNote.Rows[j].Cells[3].Value);
                    Sum += Convert.ToDouble(dataGridConsignmentNote.Rows[j].Cells[dataGridConsignmentNote.Rows[j].Cells.Count - 1].Value);
                }
                catch {
                    MessageBox.Show("В столбцax 'цена закупочная', 'цена общая' и 'кол-во' значения должны быть в числовом формате");
                
                }
            }
            label3.Text = Convert.ToString(Sum) + " p.";
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
             //переменная разрешающая перезаписывать файл только при отсуствии некорректностей
            Boolean err = true;
            for (int j = 0; j < dataGridConsignmentNote.Rows.Count - 1; j++)
            {
                for (int i = 0; i < dataGridConsignmentNote.Rows[j].Cells.Count; i++)
                {
                    //проверка на пустую ячейку
                    if (dataGridConsignmentNote.Rows[j].Cells[i].Value == null)
                    {
                        MessageBox.Show("Некоторые поля пусты" + j + "   " + i);
                        err = false;
                        break;
                    }
                    //провека на формат
                    if (i == 2 || i == 3 || i == 4)
                    {
                        try
                        {
                            Double symbol = Convert.ToDouble(dataGridConsignmentNote.Rows[j].Cells[i].Value);
                        }
                        catch
                        {
                            MessageBox.Show("В столбцax 'цена закупочная', 'цена общая' и 'кол-во' значения должны быть в числовом формате");
                            err = false;
                            break;
                        }
                    }
                }
            }
            if (err)
            {
                Double Sum = 0;
                for (int j = 0; j < dataGridConsignmentNote.Rows.Count - 1; j++)
                {
                    dataGridConsignmentNote.Rows[j].Cells[dataGridConsignmentNote.Rows[j].Cells.Count - 1].Value = Convert.ToInt32(dataGridConsignmentNote.Rows[j].Cells[2].Value) * Convert.ToDouble(dataGridConsignmentNote.Rows[j].Cells[3].Value);
                    Sum += Convert.ToDouble(dataGridConsignmentNote.Rows[j].Cells[dataGridConsignmentNote.Rows[j].Cells.Count - 1].Value);
                }
                label3.Text = Convert.ToString(Sum) + " p.";
                if (checkBox1.Checked == true)
                {
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
                }
                if (Directory.Exists(path + "\\Накладные") == false)
                {
                    Directory.CreateDirectory(path + "\\Накладные");
                    //path += @"\Накладные";
                }
                MessageBox.Show(path);
                int counfile = Directory.GetFiles(path + "\\Накладные\\").Length + 1;
                //Text =  Convert.ToString(new DirectoryInfo(path + "\\Накладные").GetFiles().Length+1);
                String Text = Convert.ToString(counfile);
                String DocName = label1.Text + " № _" + Text + "_" + DateTime.Today.ToShortDateString();
                //создаем новый документ Word
                Word.Application wdApp = new Word.Application();
                Word.Document wdDoc = null;
                Object wdMiss = System.Reflection.Missing.Value;

                wdDoc = wdApp.Documents.Add(ref wdMiss, ref wdMiss, ref wdMiss, ref wdMiss);

                // устанавливаем ориентацию (вид) документа
                wdDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

                // устанавливаем размеры полей
                wdDoc.PageSetup.TopMargin = wdApp.InchesToPoints(1);
                wdDoc.PageSetup.BottomMargin = wdApp.InchesToPoints(1);
                wdDoc.PageSetup.LeftMargin = wdApp.InchesToPoints(1);
                wdDoc.PageSetup.RightMargin = wdApp.InchesToPoints(1);

                // выводим документ на экран
                wdApp.Visible = false;

                // устанваливаем интервал между строками
                wdApp.ActiveWindow.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
                wdApp.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 30F;

                // вставляем новый параграф
                // имя параграфа
                Word.Paragraph oPara1;

                oPara1 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
                //oPara1.LineUnitAfter = 30F;
                //текст в параграфе
                string reqwiz = "";
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    reqwiz += listBox3.Items[i].ToString();
                }

                oPara1.Range.Text = reqwiz.Replace("\t", "");
                //выравнивание в документе
                oPara1.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                //размер шрифта
                oPara1.Range.Font.Size = Convert.ToInt32(14);
                oPara1.Range.InsertParagraphAfter();
                // закрываем параграф
                oPara1.Range.Paragraphs.SpaceAfter = 80;
                oPara1.CloseUp();

                // и так можно вставлять параграфв неограниченное количество
                Word.Paragraph oPara2;
                oPara2 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
                //oPara2.LineUnitAfter = 15F;
                oPara2.Range.Text = DocName;
                oPara2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                oPara2.Range.Font.Size = Convert.ToInt32(26);
                //свойство "жирности" текста
                oPara2.Range.Font.Bold = 1;
                oPara2.Range.InsertParagraphAfter();
                oPara2.Range.Paragraphs.SpaceAfter = 1;
                oPara2.CloseUp();
                oPara2.Range.Font.Bold = 0;

                object objMissing = System.Reflection.Missing.Value;
                Word.Paragraph oPara3 = wdDoc.Paragraphs.Add();
                oPara3.Range.Font.Bold = 0;
                oPara3.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                oPara3.Range.Font.Size = Convert.ToInt32(16);
                //oPara3.Range.Paragraphs.S = 1;
                Microsoft.Office.Interop.Word.Table tableObj;
                Microsoft.Office.Interop.Word.Range Rangeofword = oPara3.Range;
                //oPara3.LineUnitAfter=14;
                tableObj = wdDoc.Tables.Add(Rangeofword, dataGridConsignmentNote.RowCount, dataGridConsignmentNote.ColumnCount, ref objMissing, ref objMissing);

                for (int j = 0; j < dataGridConsignmentNote.Rows.Count; j++)
                {

                    for (int i = 0; i < dataGridConsignmentNote.Rows[j].Cells.Count; i++)
                    {
                        if (j + 1 == 1)
                        {
                            tableObj.Cell(j + 1, i + 1).Range.Font.Size = Convert.ToInt32(16);
                            tableObj.Cell(j + 1, i + 1).Range.Text = Convert.ToString(dataGridConsignmentNote.Columns[i].Name);
                            tableObj.Cell(j + 1, i + 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustifyMed;
                            tableObj.Cell(j + 1, i + 1).Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                        }
                        else
                        {
                            tableObj.Cell(j + 1, i + 1).Range.Text = Convert.ToString(dataGridConsignmentNote.Rows[j - 1].Cells[i].Value);
                            tableObj.Cell(j + 1, i + 1).Range.Font.Size = Convert.ToInt32(14);
                            tableObj.Cell(j + 1, i + 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            tableObj.Cell(j + 1, i + 1).Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }
                    }
                }
                //oPara3.Range.InsertParagraphAfter();
                oPara3.CloseUp();

                Word.Paragraph oPara4 = wdDoc.Paragraphs.Add();
                oPara4 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
                oPara4.Range.Text = "Итого: " + label3.Text;
                oPara4.Range.Paragraphs.SpaceBefore = 30;
                oPara4.Range.Font.Bold = 1;
                //выравнивание в документе
                oPara4.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                //размер шрифта
                oPara4.Range.Font.Size = Convert.ToInt32(20);
                //oPara4.Range.InsertParagraphAfter();
                // закрываем параграф
                oPara4.CloseUp();

                Word.Paragraph oPara5 = wdDoc.Paragraphs.Add();
                oPara5 = wdDoc.Content.Paragraphs.Add(ref wdMiss);
                oPara5.Range.Text = "Отв. лицо: " + Form2.UserName;
                oPara5.Range.Paragraphs.SpaceBefore = 30;
                oPara5.Range.Font.Bold = 0;
                //выравнивание в документе
                oPara5.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                //размер шрифта
                oPara5.Range.Font.Size = Convert.ToInt32(16);
                //oPara5.Range.InsertParagraphAfter();
                // закрываем параграф
                oPara5.CloseUp();
                // Сохранение документа в файл
                try
                {
                    // можно прописать полный путь сохранения к файлу
                    // по-умолчанию, файл сохраняется в мои документы
                    object filename = @path + @"\Накладные\" + DocName + ".doc";

                    //сохраняем документ на диске
                    wdDoc.SaveAs(ref filename);

                    // Закрываем текущий документ
                    wdDoc.Close(ref wdMiss, ref wdMiss, ref wdMiss);
                    wdDoc = null;

                    // Закрываем приложение Word
                    wdApp.Quit(ref wdMiss, ref wdMiss, ref wdMiss);
                    wdDoc = null;
                }
                catch (Exception y)
                {
                    MessageBox.Show("Ошибка сохранения Word документа\n" + y.ToString());

                }
                try
                {
                    Boolean tr = true;
                    using (StreamWriter writer = new StreamWriter("Заказы на склад.txt", true, Encoding.GetEncoding("windows-1251")))
                    {
                        for (int j = 0; j < dataGridConsignmentNote.Rows.Count; j++)
                        {
                            for (int i = 0; i < dataGridConsignmentNote.Rows[j].Cells.Count; i++)
                            {
                                if (j == 0)
                                {
                                    //if (tr)
                                    //{
                                    //    tr = false;
                                    //    writer.Write("\n");
                                    //}
                                }
                                else if (i == dataGridConsignmentNote.Rows[j].Cells.Count - 1)
                                {
                                    writer.Write(dataGridConsignmentNote.Rows[j - 1].Cells[i].Value);
                                }
                                else
                                {
                                    writer.Write(dataGridConsignmentNote.Rows[j - 1].Cells[i].Value + ";");
                                }
                            }
                            if (j != 0)
                            {
                                writer.WriteLine();
                            }
                        }
                        MessageBox.Show("Файл \"Заказ на склад.txt\" успешно обновлён.");
                    }
                }
                catch (Exception y)
                {
                    MessageBox.Show("Ошибка обновления файла \"Заказ на склад.txt\"\n" + y.ToString());
                
                }
            }
        }

        private void ConsignmentNote_Load(object sender, EventArgs e)
        {
            string input="Название препарата;Поставщик;Кол-во;Цена закупочная;Цена общая";
            string[] subs = input.Split(';');

            for (int i = 0; i < subs.Length; i++)
            {
                dataGridConsignmentNote.Columns.Add(subs[i], subs[i]);
                //dataGridViewPreparats.Font = new Font("Verbena", 8);
                //dataGridConsignmentNote.Columns[i].Width = 100;

            }

        }

  
    }
}
