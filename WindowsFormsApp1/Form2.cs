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
    public partial class Form2 : Form
    {

        public TextBox[] Radius;
        public TextBox[] LocationX;
        public TextBox[] LocationY;
        public int[] R;
        public Form2(int NumAEC, bool checked1, bool checked2, bool checked3, bool checked4)
        {
            InitializeComponent();
            Radius = new TextBox[NumAEC];
            LocationX = new TextBox[NumAEC];
            LocationY = new TextBox[NumAEC];
            R = new int[NumAEC];
            for (int i = 0; i < NumAEC; i++)
            {
                Radius[i] = new TextBox();
                Radius[i].Location = new System.Drawing.Point(200, 35 * i + 20);
                if (i >= 4)
                {
                    Radius[i].Location = new System.Drawing.Point(500, 35 * (i - 4) + 20);
                }
                this.Controls.Add(Radius[i]); // Добавляем текстовое поле на форму
                Radius[i].TextChanged += RadiusTextBox_TextChanged;
            }
            
            for (int i = 0; i < NumAEC; i++)
            {
                Label l1 = new Label();
                l1.Location = new System.Drawing.Point(50, 35 * i + 20);
                if (i >= 4)
                {
                    l1.Location = new System.Drawing.Point(350, 35 * (i - 4) + 20);
                }
                l1.AutoSize = true;
                l1.Text = $"Введіть радіус для R {i + 1}";
                this.Controls.Add(l1);
            }
            for (int i = 0; i < NumAEC; i++)
            {
                if (!string.IsNullOrEmpty(Radius[i].Text))
                {
                    if (int.TryParse(Radius[i].Text, out int b))
                    {
                        R[i] = b;
                    }
                }
            }

            if (checked2 == true)
            {
                for (int i = 0; i < NumAEC; i++)
                {

                }
            }


        }
        private void RadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender; // Получаем TextBox, вызвавший событие
            int index = Array.IndexOf(Radius, textBox); // Находим индекс измененного TextBox
            if (index != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (int.TryParse(textBox.Text, out int radiusValue))
                {
                    R[index] = radiusValue;
                }
            }
        }
    
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(R);
            f3.Show();
        }

    }
}
