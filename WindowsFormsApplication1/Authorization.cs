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
using System.Globalization;
using System.Text.RegularExpressions;

namespace AISHospitalPharmacy
{
    public partial class Form2 : Form
    {
        public static String usr = null;
        public static String UserName = null;
        public Form2()
        {
            InitializeComponent();
            
        }

        public void button2_Click(object sender, EventArgs e)
        {
            //если поля пустые ничего не делаем
            if (textBox1.Text != String.Empty || textBox2.Text != String.Empty)
            {
                //Находим соответсвующие логин и парль в бд пользователей  
                //открываем файл, читаем построчной, делим строку по символу ищем совпадения в определённых колонках
                var find = new StreamReader("Сотрудники.txt", Encoding.GetEncoding(1251));
                using (find)
                {
                    String input = null;
                    String[] subsw = null;
                    int fl = 0;
                    while ((input = find.ReadLine()) != null)
                    {
                        subsw = input.Split(';');
                        String PasswordFile = subsw[8];
                        String LoginFile = subsw[7];
                        if (LoginFile.IndexOf(textBox1.Text) > -1 && PasswordFile.IndexOf(textBox2.Text) > -1)
                        {
                            fl = 1;
                            UserName = subsw[0];
                            usr = subsw[9];
                            break;
                        }

                    }
                    if (fl == 1)
                    {
                        fl = 0;
                        MainForm f = new MainForm();
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверен логин или пароль");

                    }
                }
            }
            //else { MessageBox.Show("Поля пусты"); }
        }
    }
}
