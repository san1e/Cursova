using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int NumAEC;

        public int NumOP;
        public Form1()
        {
            InitializeComponent();
            
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                // Додаткова логіка, яка виконується, коли checkbox1 вибраний
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                // Додаткова логіка, яка виконується, коли checkbox2 вибраний
            }
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.KeyPress += textBox_KeyPress;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (int.TryParse(textBox1.Text, out int a))
                {
                    NumAEC = a;
                }
                
            }
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.KeyPress += textBox_KeyPress;
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                if (int.TryParse(textBox2.Text, out int b))
                {
                    NumOP = b;
                }
               
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
            {
                checkBox4.Checked = false;
                // Додаткова логіка, яка виконується, коли checkbox1 вибраний
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox3.Checked = false;
                // Додаткова логіка, яка виконується, коли checkbox2 вибраний
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Перевірка чи натискана клавіша є цифрою або клавішею Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ігноруємо введення, якщо це не цифра або Backspace
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (checkBox1.Checked == false && checkBox2.Checked == false)
            //{
            //    MessageBox.Show("Оберіть варіант заповнення для АЕС");
            //}
            //else if (checkBox3.Checked == false && checkBox4.Checked == false)
            //{
            //    MessageBox.Show("Оберіть варіант заповнення для ПС");
            //}
            //else if (NumAEC > 4 )
            //{
            //    MessageBox.Show("Введіть меньшу кількість АЕС");

            //}
            //else if (NumOP > 10 )
            //{
            //    MessageBox.Show("Введіть меньшу кількість ПС");

            //}
            //else if (NumAEC == 0)
            //{
            //    MessageBox.Show("Введіть кількість АЕС");

            //}
            //else if (NumOP==0)
            //{
            //    MessageBox.Show("Введіть кількість ПС");

            //}
            //else
            //{
                Form2 f2 = new Form2(NumAEC, NumOP,checkBox1.Checked, checkBox2.Checked,checkBox3.Checked, checkBox4.Checked);
                f2.AutoSize = true;
                f2.Show();
            //}


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
