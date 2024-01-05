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
    public partial class MainWindow : Form
    {
        public int NumAEC;

        public int NumOP;
        public MainWindow()
        {
            InitializeComponent();
            
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //автоматичне заповнення АЕС
        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }
        //ручне заповнення АЕС
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }

        //автоматичне заповнення ПС
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
            {
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }
        //ручне заповнення ПС
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox3.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox6.Checked = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
            else if (checkBox5.Checked == false)
            {
                checkBox6.Checked = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox5.Checked = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
            else if (checkBox6.Checked == false)
            {
                checkBox5.Checked = false;
            }
        }

        //кількість АЕС
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
        // кількість ПС
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
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox5.Checked == false)
            {
                MessageBox.Show("Оберіть варіант заповнення для АЕС");
            }
            else if (checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false)
            {
                MessageBox.Show("Оберіть варіант заповнення для пунктів контролю");
            }
            else if (NumAEC > 4)
            {
                MessageBox.Show("Введіть меньшу кількість АЕС");

            }
            else if (NumOP > 10)
            {
                MessageBox.Show("Введіть меньшу кількість пунктів контролю");

            }
            else if (NumAEC == 0)
            {
                MessageBox.Show("Введіть кількість АЕС");

            }
            else if (NumOP == 0)
            {
                MessageBox.Show("Введіть кількість пунктів контролю");

            }
            else
            {
                InputWindow f2 = new InputWindow(NumAEC, NumOP, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked,checkBox5.Checked);
                f2.AutoSize = true;
                f2.Show();
            }


        }
        
    }
}
