﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class InputWindow : Form
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
        int NAEC;
        int NOP;
        bool ck2;
        bool ck4;
        bool ck5;

        private void RadiusInput(TextBox[] Radius, int[] R,int NumAEC, bool checked2)
        {
            for (int i = 0; i < NumAEC; i++)
            {
                Label l1 = new Label();
                l1.Location = new System.Drawing.Point(50, 35 * i + 20);
                l1.AutoSize = true;
                l1.Text = $"Введіть радіус для R {i + 1}";

                b1.Location = new System.Drawing.Point(380, 150);

                Radius[i] = new TextBox();
                Radius[i].Location = new System.Drawing.Point(200, 35 * i + 20);

                if (i >= 2)
                {

                    if (checked2 == true)
                    {
                        l1.Location = new System.Drawing.Point(450, 35 * (i - 2) + 20);
                        Radius[i].Location = new System.Drawing.Point(600, 35 * (i - 2) + 20);
                    }
                    else
                    {
                        Radius[i].Location = new System.Drawing.Point(500, 35 * (i - 2) + 20);
                        l1.Location = new System.Drawing.Point(350, 35 * (i - 2) + 20);
                    }

                }
                this.Controls.Add(Radius[i]);
                this.Controls.Add(l1);
                this.Controls.Add(b1);
                Radius[i].KeyPress += textBox_KeyPress;
                Radius[i].TextChanged += RadiusTextBox_TextChanged;

               
            }
        }

        private void RandomAEC(int NumAEC)
        {
            Random rnd = new Random();
          for (int i = 0; i < NumAEC; i++)
          {

             XAEC[i] = rnd.Next(1, 15) * 50;
             YAEC[i] = rnd.Next(1, 15) * 50;
          }
            
        }

        private void RandomOP(int NumOP)
        {
            Random rnd = new Random();
            for (int i = 0; i < NumOP; i++)
            {
                XOP[i] = rnd.Next(1, 15) * 50;
                YOP[i] = rnd.Next(1, 15) * 50;
            }
        }

        private void HandlyAECInput( int NumAEC)
        {
            Label l3;
            Label l4;
            for (int i = 0; i < NumAEC; i++)
            {
                l3 = new Label();
                l3.Location = new System.Drawing.Point(330, 5);
                l3.Text = "X";
                l3.AutoSize = true;

                l4 = new Label();
                l4.Location = new System.Drawing.Point(390, 5);
                l4.Text = "Y";
                l4.AutoSize = true;

                LocationX[i] = new TextBox();
                LocationY[i] = new TextBox();

                LocationX[i].Location = new System.Drawing.Point(320, 35 * i + 20);
                LocationX[i].Size = new System.Drawing.Size(40, 20);

                LocationY[i].Location = new System.Drawing.Point(380, 35 * i + 20);
                LocationY[i].Size = new System.Drawing.Size(40, 20);

                if (i >= 2)
                {
                    LocationX[i].Location = new System.Drawing.Point(720, 35 * (i - 2) + 20);
                    LocationY[i].Location = new System.Drawing.Point(780, 35 * (i - 2) + 20);

                    l3.Location = new System.Drawing.Point(730, 5);
                    l4.Location = new System.Drawing.Point(790, 5);

                }
                this.Controls.Add(LocationX[i]);
                this.Controls.Add(LocationY[i]);
                this.Controls.Add(l3);
                this.Controls.Add(l4);

                LocationX[i].TextChanged += XAECTextBox_TextChanged;
                LocationY[i].TextChanged += YAECTextBox_TextChanged;

                LocationX[i].KeyPress += textBox_KeyPress;
                LocationY[i].KeyPress += textBox_KeyPress;

                
            }
        }

        private void HandlyOPInput(int NumOP)
        {
            Label l3;
            Label l4;
            int n = 35;
            b1.Location = new System.Drawing.Point(380, 350);
            for (int i = 0; i < NumOP; i++)
            {
                Label l2 = new Label();
                l2.Location = new System.Drawing.Point(50, 135 + n * i + 20);
                l2.AutoSize = true;
                l2.Text = $"Введіть координати для {i + 1} Пункта Спостереження";

                l3 = new Label();
                l3.Location = new System.Drawing.Point(330, 140);
                l3.AutoSize = true;
                l3.Text = "X";
                l4 = new Label();
                l4.Location = new System.Drawing.Point(390, 140);
                l4.AutoSize = true;
                l4.Text = "Y";

                LocationOPX[i] = new TextBox();
                LocationOPY[i] = new TextBox();

                LocationOPX[i].Location = new System.Drawing.Point(320, 135 + n * i + 20);
                LocationOPX[i].Size = new System.Drawing.Size(40, 20);

                LocationOPY[i].Location = new System.Drawing.Point(380, 135 + n * i + 20);
                LocationOPY[i].Size = new System.Drawing.Size(40, 20);

                if (i >= 5)
                {
                    LocationOPX[i].Location = new System.Drawing.Point(720, 135 + n * (i - 5) + 20);
                    LocationOPY[i].Location = new System.Drawing.Point(780, 135 + n * (i - 5) + 20);

                    l2.Location = new System.Drawing.Point(450, 135 + n * (i - 5) + 20);
                    l3.Location = new System.Drawing.Point(730, 140);
                    l4.Location = new System.Drawing.Point(790, 140);

                }
                this.Controls.Add(LocationOPX[i]);
                this.Controls.Add(LocationOPY[i]);
                this.Controls.Add(l2);
                this.Controls.Add(l3);
                this.Controls.Add(l4);
                this.Controls.Add(b1);

                LocationOPX[i].TextChanged += XOPTextBox_TextChanged;
                LocationOPY[i].TextChanged += YOPTextBox_TextChanged;

                LocationOPX[i].KeyPress += textBox_KeyPress;
                LocationOPY[i].KeyPress += textBox_KeyPress;
                
            }
        }

        public InputWindow(int NumAEC,int NumOP,bool checekd1, bool checked2,bool checked3, bool checked4, bool checked5)
        {
            InitializeComponent();
            Radius = new TextBox[NumAEC];
            LocationX = new TextBox[NumAEC];
            LocationY = new TextBox[NumAEC];
            LocationOPX= new TextBox[NumOP];
            LocationOPY= new TextBox[NumOP];
            R = new int[NumAEC];
            XAEC = new int[NumAEC];
            YAEC = new int[NumAEC];
            XOP = new int[NumOP];
            YOP = new int[NumOP];
            NAEC = NumAEC ;
            NOP = NumOP;
            ck2 = checked2;
            ck4 = checked4;
            ck5 = checked5;


            b1.Click += b1_Click;

            RadiusInput(Radius,R,NumAEC,checked2);

            if (checekd1== true)
            {
                RandomAEC(NumAEC);
            }
            //ручне введення координатів АЕС
            if (checked2 == true)
            {
                HandlyAECInput(NumAEC);
            }

            if (checked3 == true)
            {
                RandomOP(NumOP);
            }

            //ручне введення координатів ПС.
            
            if (checked4 == true) 
            {
                HandlyOPInput(NumOP);
            }


        }
        private void RadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int index = Array.IndexOf(Radius, textBox);
            if (index != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (int.TryParse(textBox.Text, out int radiusValue))
                {
                    R[index] = radiusValue*50;
                }
            }
        }

        private void XAECTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender; 
            int index = Array.IndexOf(LocationX, textBox); 
            if (index != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (int.TryParse(textBox.Text, out int Xcor))
                {
                    XAEC[index] = Xcor*50;
                }
            }
        }

        private void YAECTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int index = Array.IndexOf(LocationY, textBox);
            if (index != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (int.TryParse(textBox.Text, out int Ycor))
                {
                    YAEC[index] = Ycor * 50;
                }
            }
        }

        private void XOPTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender; 
            int index = Array.IndexOf(LocationOPX, textBox); 
            if (index != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (int.TryParse(textBox.Text, out int XOPcor))
                {
                    XOP[index] = XOPcor * 50;
                }
            }
        }

        private void YOPTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender; 
            int index = Array.IndexOf(LocationOPY, textBox); 
            if (index != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (int.TryParse(textBox.Text, out int YOPcor))
                {
                    YOP[index] = YOPcor * 50;
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Перевірка чи натискана клавіша є цифрою або клавішею Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if ((ck2 == true && ck4 == true) || (ck2 == true && ck4 == false) || (ck2==false&&ck4==true))
            {
                MessageBox.Show("Мінімум для Х і У = 1\nМаксимум для Х і У = 15\nМаксимальне значення для R = 7");

            }
            else
            {
                MessageBox.Show("Максимальне значення для R = 7");

            }
           

        }

        private void b1_Click(object sender, EventArgs e)
        {
            bool validationFailed = false;
            for (int i = 0; i < NAEC; i++)
            {
                if (R[i] == 0)
                {
                    MessageBox.Show($"Введіть значення для R{i + 1}");
                    validationFailed = true;
                }
                else if (XAEC[i] == 0 && ck5 == false)
                {
                    MessageBox.Show($"Введіть значення для АЕС Х{i + 1}");
                    validationFailed = true;
                }
                else if (YAEC[i] == 0 && ck5 == false)
                {
                    MessageBox.Show($"Введіть значення для АЕС Y{i + 1}");
                    validationFailed = true;
                }
                else if (R[i] > 7 * 50)
                {
                    MessageBox.Show($"Введіть меньше значення для R{i + 1}");
                    validationFailed = true;
                }
                else if (XAEC[i] > 15 * 50 && ck5 == false)
                {
                    MessageBox.Show($"Введіть меньше значення для АЕС Х{i + 1}");
                    validationFailed = true;
                }
                else if (YAEC[i] > 15 * 50 && ck5 == false)
                {
                    MessageBox.Show($"Введіть меньше значення для АЕС Y{i + 1}");
                    validationFailed = true;
                }
                

            }

            for (int i = 0; i < NOP; i++)
            {
                if (XOP[i] == 0 && ck5 == false)
                {
                    MessageBox.Show($"Введіть значення для пункту контролю Х{i + 1}");
                    validationFailed = true;
                }
                else if (YOP[i] == 0 && ck5 == false)
                {
                    MessageBox.Show($"Введіть значення для ПС Y{i + 1}");
                    validationFailed = true;
                }
                else if (XOP[i] > 15 * 50 && ck5 == false)
                {
                    MessageBox.Show($"Введіть меньше значення для ПС Х{i + 1}");
                    validationFailed = true;
                }
                else if (YOP[i] > 15 * 50 && ck5 == false)
                {
                    MessageBox.Show($"Введіть меньше значення для ПС Y{i + 1}");
                    validationFailed = true;
                }
                
            }

            if (!validationFailed && ck5 == false)
            {
                OutputWindow1 f3 = new OutputWindow1(R, XAEC, YAEC, XOP, YOP, NAEC, NOP);
                f3.AutoSize = true;
                f3.Show();
            }
            else if (!validationFailed && ck5 == true)
            {
                OutputWindow2 f4 = new OutputWindow2(R,NAEC,NOP);
                f4.AutoSize = true;
                f4.Show();
            }

        }

    }
}
