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

        public TextBox[] LocationOPX;
        public TextBox[] LocationOPY;
        public int[] R;
        public int[] XAEC;
        public int[] YAEC;
        public int[] XOP;
        public int[] YOP;
        public Form2(int NumAEC,int NumOP, bool checked2, bool checked4)
        {
            InitializeComponent();
            Radius = new TextBox[NumAEC];
            LocationX = new TextBox[NumAEC];
            LocationY = new TextBox[NumAEC];
            LocationOPX= new TextBox[NumOP];
            LocationOPY= new TextBox[NumOP];
            R = new int[NumAEC];
            

            for (int i = 0; i < NumAEC; i++)
            {
                Label l1 = new Label();
                l1.Location = new System.Drawing.Point(50, 35 * i + 20);
                l1.AutoSize = true;
                l1.Text = $"Введіть радіус для R {i + 1}";

                Radius[i] = new TextBox();
                Radius[i].Location = new System.Drawing.Point(200, 35 * i + 20);

                if (i >= 3)
                {
                    
                    if (checked2 == true)
                    {
                        l1.Location = new System.Drawing.Point(450, 35 * (i - 3) + 20);
                        Radius[i].Location = new System.Drawing.Point(600, 35 * (i - 3) + 20);
                    }
                    else 
                    {
                        Radius[i].Location = new System.Drawing.Point(500, 35 * (i - 3) + 20);
                        l1.Location = new System.Drawing.Point(350, 35 * (i - 3) + 20);
                    }
                    
                }
                this.Controls.Add(Radius[i]); // Добавляем текстовое поле на форму
                Radius[i].TextChanged += RadiusTextBox_TextChanged;
                this.Controls.Add(l1);

                if (!string.IsNullOrEmpty(Radius[i].Text))
                {
                    if (int.TryParse(Radius[i].Text, out int b))
                    {
                        R[i] = b;
                    }
                }
            }
            
            //ручне введення координатів АЕС
            if (checked2 == true)
            {
                for (int i = 0; i < NumAEC; i++)
                {
                    LocationX[i] = new TextBox();
                    LocationY[i] = new TextBox();

                    LocationX[i].Location = new System.Drawing.Point(320, 35 * i + 20);
                    LocationX[i].Size = new System.Drawing.Size(40, 20);

                    LocationY[i].Location = new System.Drawing.Point(380,35*i+20);
                    LocationY[i].Size = new System.Drawing.Size(40,20);

                    if (i >= 3 )
                    {
                        LocationX[i].Location = new System.Drawing.Point(720, 35 * (i-3) + 20);
                        LocationY[i].Location = new System.Drawing.Point(780, 35 * (i-3) + 20);
                    }
                    this.Controls.Add(LocationX[i]);
                    this.Controls.Add(LocationY[i]);
                    LocationX[i].TextChanged += RadiusTextBox_TextChanged;
                    LocationY[i].TextChanged += RadiusTextBox_TextChanged;
                    if (!string.IsNullOrEmpty(LocationX[i].Text)& !string.IsNullOrEmpty(LocationY[i].Text))
                    {
                        if (int.TryParse(LocationX[i].Text, out int x) & int.TryParse(LocationY[i].Text,out int y))
                        {
                            XAEC[i]=x;
                            YAEC[i]=y;
                        }
                    }
                }
            }

            //ручне введення координатів ПС.
            int n = 35;
            if (checked4 == true) 
            {
                
                for (int i = 0; i < NumOP; i++)
                {
                    Label l2 = new Label();
                    l2.Location = new System.Drawing.Point(50, 135 + n * i + 20);
                    l2.AutoSize = true;
                    l2.Text = $"Введіть координати для {i + 1} Пункта Спостереження";

                    LocationOPX[i] = new TextBox();
                    LocationOPY[i] = new TextBox();

                    LocationOPX[i].Location = new System.Drawing.Point(320, 135 + n*i + 20);
                    LocationOPX[i].Size = new System.Drawing.Size(40, 20);

                    LocationOPY[i].Location = new System.Drawing.Point(380, 135 + n*i + 20);
                    LocationOPY[i].Size = new System.Drawing.Size(40, 20);
                   
                    if (i >= 5)
                    {
                        LocationOPX[i].Location = new System.Drawing.Point(720, 135 + n*(i - 5) + 20);
                        LocationOPY[i].Location = new System.Drawing.Point(780, 135 + n*(i - 5) + 20);
                        l2.Location = new System.Drawing.Point(450, 135 + n * (i-5) + 20);
                    }
                    this.Controls.Add(LocationOPX[i]);
                    this.Controls.Add(LocationOPY[i]);
                    this.Controls.Add(l2);
                    LocationOPX[i].TextChanged += RadiusTextBox_TextChanged;
                    LocationOPY[i].TextChanged += RadiusTextBox_TextChanged;
                    if (!string.IsNullOrEmpty(LocationOPX[i].Text) & !string.IsNullOrEmpty(LocationOPY[i].Text))
                    {
                        if (int.TryParse(LocationOPX[i].Text, out int x) & int.TryParse(LocationOPY[i].Text, out int y))
                        {
                            XOP[i] = x;
                            YOP[i] = y;
                        }
                    }
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
