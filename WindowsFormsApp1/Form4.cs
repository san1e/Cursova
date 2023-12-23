using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {

        int NumAEC_;
        int NumOP_;
        private List<int> XAEC = new List<int>();
        private List<int> YAEC = new List<int>();
        private List<int> XOP = new List<int>();
        private List<int> YOP = new List<int>();
        private List<int> intersectingAECIndices = new List<int>();
        int[] R_;
        int opInMultipleCirclesCount_ = 0;
        int builtObservationPointsCount = 0;


        public Form4(int[] R, int NumAEC, int NumOP)
        {
            InitializeComponent();
            R_ = new int[R.Length];
            for (int i = 0; i < R.Length; i++)
            {
                R_[i] = R[i];
            }
            NumAEC_ = NumAEC;
            NumOP_ = NumOP;
            this.MouseDown += MainForm_MouseDown;
            this.Paint += MainForm_Paint;
            
            
        }


        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            {
                int roundedX = (int)(Math.Round((double)e.X / 50) * 50);
                int roundedY = (int)(Math.Round((double)e.Y / 50) * 50);

                if (roundedX >= 0 && roundedX <= 750 && roundedY >= 0 && roundedY <= 750)
                {
                    if (checkBox1.Checked == true)
                    {
                        if (XAEC.Count < NumAEC_)
                        {
                            XAEC.Add(roundedX);
                            YAEC.Add(roundedY);
                            P_AEC(roundedX, roundedY);
                        }
                        else
                        {
                            MessageBox.Show("Максимальна кількість АЕС");
                        }
                    }
                    else if (checkBox2.Checked == true)
                    {
                        if (XOP.Count < NumOP_)
                        {
                            XOP.Add(roundedX);
                            YOP.Add(roundedY);
                            P_OP(roundedX, roundedY);
                        }
                        else
                        {
                            MessageBox.Show("Максимальна кількість OP");
                        }
                    }
                }
                this.Invalidate();
            }
        }

        private void P_AEC(int x, int y)
        {
            PictureBox PicAEC = new PictureBox();
            PicAEC.Image = Properties.Resources.AEC_;
            PicAEC.Size = new Size(41, 41);
            PicAEC.BackColor = Color.White;
            PicAEC.SizeMode = PictureBoxSizeMode.Zoom;
            PicAEC.Location = new Point(x - PicAEC.Width / 2, y - PicAEC.Height / 2);
            Controls.Add(PicAEC);
        }

        private void P_OP(int x, int y)
        {
            PictureBox PicOP = new PictureBox();
            PicOP.Image = Properties.Resources.OP_;
            PicOP.Size = new Size(21, 21);
            PicOP.BackColor = Color.White;
            PicOP.SizeMode = PictureBoxSizeMode.Zoom;
            PicOP.Location = new Point(x - PicOP.Width / 2, y - PicOP.Height / 2);
            Controls.Add(PicOP);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }

        public void Res()
        {
            int[] res1 = new int[NumAEC_];
            int opNotInAnyCircleCount = 0;
            int[] circlesContainingOP = new int[NumOP_];
            for (int i = 0; i < XOP.Count; i++)
            {
                int xOP = XOP[i] + (21 / 2);
                int yOP = YOP[i] + (21 / 2);
                Array.Clear(circlesContainingOP, 0, circlesContainingOP.Length);
                for (int j = 0; j < NumAEC_; j++)
                {
                    int xAEC = XAEC[j];
                    int yAEC = YAEC[j];
                    int radiusAEC = R_[j];
                    if (IsPointInCircle(xOP, yOP, xAEC, yAEC, radiusAEC))
                    {
                        res1[j]++;
                        circlesContainingOP[i]++;
                        intersectingAECIndices.Add(j);

                    }
                }
                if (circlesContainingOP[i] == 0)
                {
                    opNotInAnyCircleCount++;
                }

                if (circlesContainingOP[i] > 1)
                {
                    opInMultipleCirclesCount_++;
                }

            }
            ResOutput(res1,opNotInAnyCircleCount,opInMultipleCirclesCount_);

        }

     

        private void ResOutput(int[] res1,int opNotInAnyCircleCount, int opInMultipleCirclesCount)
        {
            Label[] labels = new Label[NumAEC_];
            for (int i = 0; i < NumAEC_; i++)
            {
                labels[i] = new Label();
                labels[i].Location = new Point(920, 40 + i * 20);
                labels[i].Text = $"Кількість пунктів спостереження, які потрапили в радіус АЕС {i + 1}: {res1[i]}";
                labels[i].AutoSize = true;
                Controls.Add(labels[i]);
            }

            Label l = new Label();
            l.Location = new Point(920, 20);
            l.Text = $"Кількість пунктів спостереження, які не використовуються: {opNotInAnyCircleCount}";
            l.AutoSize = true;
            Controls.Add(l);

            Label l1 = new Label();
            l1.Location = new Point(920, 0);
            l1.Text = $"Кількість пункітв спостереження, які знаходяться на перетені: {opInMultipleCirclesCount}";
            l1.AutoSize = true;
            Controls.Add(l1);



        }



        private void OutSideOP()
        {
            if (builtObservationPointsCount >= opInMultipleCirclesCount_)
            {
                MessageBox.Show($"Досягнуто максимальну кількість точок: {opInMultipleCirclesCount_}");
                return;
            }

            Random rnd = new Random();
            int x = rnd.Next(1, 15) * 50;
            int y = rnd.Next(1, 15) * 50;

            // Перевірка, чи точка поза межами будь-якого кола
            bool outsideCircles = true;
            for (int j = 0; j < NumAEC_; j++)
            {
                int xAEC = XAEC[j];
                int yAEC = YAEC[j];
                int radiusAEC = R_[j];
                if (IsPointInCircle(x, y, xAEC, yAEC, radiusAEC))
                {
                    outsideCircles = false;
                    break;
                }
            }

            // Якщо точка поза межами всіх кол та кількість ще не досягла ліміту, то вивести її
            if (outsideCircles)
            {
                P_OP(x, y);
                builtObservationPointsCount++;
            }
        }
        static bool IsPointInCircle(int pointX, int pointY, int circleX, int circleY, int radius)
        {
            double distance = Math.Sqrt(Math.Pow(pointX - circleX, 2) + Math.Pow(pointY - circleY, 2));
            return distance <= radius;
        }



        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 50; i <= 750; i += 50)
            {
                g.DrawLine(new Pen(Color.Black), i, 0, i, 750);
                g.DrawLine(new Pen(Color.Black), 0, i, 750, i);
            }

            for (int i = 0; i < XAEC.Count; i++)
            {
                int x = XAEC[i];
                int y = YAEC[i];
                int radius = R_[i];

                int circleX = x - radius;
                int circleY = y - radius;
                int circleDiameter = 2 * radius;

                Rectangle clipRect = new Rectangle(0, 0, 750, 750);
                g.SetClip(clipRect);
                g.DrawArc(new Pen(Color.Black, 5), circleX, circleY, circleDiameter, circleDiameter, 0, 360);
                g.ResetClip();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (XAEC.Count < NumAEC_)
            {
                MessageBox.Show("Не всі АЕС знаходяться на координатній площині");
            }
            else if (XOP.Count < NumOP_)
            {
                MessageBox.Show("Не всі ПС знаходяться на координатній площині");
            }
            else
            {
                Res();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

                OutSideOP();
        }
    }
}


