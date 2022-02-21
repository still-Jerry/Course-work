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
    public partial class Form7 : Form
    {
        public static String userInfo = null;
        public static Int32 Index = -1;
        public static bool fl1 = false;

        public Form7()
        {
            InitializeComponent();
            button7.Enabled = false;
            button8.Enabled = false;
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите покинуть данную страницу и сохранить все изменеия?", "Сохранение изменений",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete("Сотрудники1.txt");
               
                using (StreamWriter writer = new StreamWriter("Сотрудники1.txt", true, Encoding.GetEncoding("windows-1251")))
                //using (StreamWriter writer = File.AppendText("Сотрудники.txt"))
                {
                    for (int i = 0; i < listBox3.Items.Count; i++)
                    {
                        writer.WriteLine(listBox3.Items[i]);
                    }
                    

                } 
            }
            
            MainForm f = new MainForm();
            f.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddNewUsers f = new AddNewUsers();
            f.Show();
            this.Hide();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            var find = new StreamReader("Сотрудники.txt", Encoding.GetEncoding(1251));
            //using (StreamReader find = File.OpenText("Сотрудники.txt"))
            using (find)
            {
                String input = null;
                while ((input = find.ReadLine()) != null)
                {
                    listBox3.Items.Add(input); 
                }
                
            }
            if (fl1) {
                listBox3.Items.RemoveAt(Index);
                listBox3.Items.Insert(Index, userInfo);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить сотрудника из БД?", "Удаление сотрудника",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                }
                
            }
            catch(Exception){
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex == -1)
            {
                button7.Enabled = false;
                button8.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
                button8.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            userInfo = Convert.ToString(listBox3.SelectedItem);
            Index = listBox3.SelectedIndex;
            Form8 f = new Form8();
            f.Show();
            this.Hide();
        }
    }
}
