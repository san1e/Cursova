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
    public partial class OutputWindow2 : Form
    {

        int NumAEC_;
        int NumOP_;
        private List<int> XAEC = new List<int>();
        private List<int> YAEC = new List<int>();
        private List<int> XOP = new List<int>();
        private List<int> YOP = new List<int>();

        List<int> intersectingCircles = new List<int>();
        List<int> opInMultipleCirclesIndexes = new List<int>();
        int[] R_;
        int opInMultipleCirclesCount_ = 0;
        int builtObservationPointsCount = 0;


        public OutputWindow2(int[] R, int NumAEC, int NumOP)
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
            button2.Enabled = false;


        }


        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            {
                int X = e.X;
                int Y = e.Y;
                X = RoundToNearest(X, 50);
                Y = RoundToNearest(Y, 50);

                if (X >= 0 && X <= 750 && Y >= 0 && Y <= 750)
                {
                    if (checkBox1.Checked == true)
                    {
                        if (XAEC.Count < NumAEC_)
                        {
                            XAEC.Add(X);
                            YAEC.Add(Y);
                            P_AEC(X, Y);
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
                            XOP.Add(X);
                            YOP.Add(Y);
                            P_OP(X, Y);
                        }
                        else
                        {
                            MessageBox.Show("Максимальна кількість пунктів контролю");
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
                        intersectingCircles.Add(j);

                    }
                }
                if (circlesContainingOP[i] == 0)
                {
                    opNotInAnyCircleCount++;
                }

                if (circlesContainingOP[i] > 1)
                {
                    opInMultipleCirclesIndexes.Add(i);
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
                labels[i].Text = $"Кількість пунктів контролю, які потрапили в радіус АЕС {i + 1}: {res1[i]}";
                labels[i].AutoSize = true;
                Controls.Add(labels[i]);
            }

            Label l = new Label();
            l.Location = new Point(920, 20);
            l.Text = $"Кількість пунктів контролю, які не використовуються: {opNotInAnyCircleCount}";
            l.AutoSize = true;
            Controls.Add(l);

            Label l1 = new Label();
            l1.Location = new Point(920, 0);
            l1.Text = $"Кількість пункітв контролю, які знаходяться на перетені: {opInMultipleCirclesCount}";
            l1.AutoSize = true;
            Controls.Add(l1);



        }


        private int RoundToNearest(int value, int roundTo)
        {
            return (int)(Math.Round((double)value / roundTo) * roundTo);
        }
        private void OutSideOP()
        {
            if (builtObservationPointsCount >= opInMultipleCirclesCount_)
            {
                MessageBox.Show($"Досягнуто максимальну кількість точок: {opInMultipleCirclesCount_}");
                return;
            }

            Random rnd = new Random();

            // Перевірка, чи точка поза межами будь-якого кола
            bool outsideCircles = true;
            int x, y;

            do
            {
                // Виберіть випадкову точку на колі або в його центрі
                int randomCircleIndex1 = rnd.Next(NumAEC_);
                int randomCircleIndex2 = rnd.Next(NumAEC_);
                int xAEC = (XAEC[randomCircleIndex1]+XAEC[randomCircleIndex2])/2;
                int yAEC = (YAEC[randomCircleIndex1] + YAEC[randomCircleIndex2]) / 2;
                int radiusAEC = (R_[randomCircleIndex1]+R_[randomCircleIndex2])/2;

                double angle = rnd.NextDouble() * 2 * Math.PI;
                double distance = rnd.Next(radiusAEC + 1, 2 * radiusAEC); // Виберіть випадковий радіус

                x = (int)(xAEC + distance * Math.Cos(angle));
                y = (int)(yAEC + distance * Math.Sin(angle));
                
                // Округлення координат до 50
                x = RoundToNearest(x, 50);
                y = RoundToNearest(y, 50);

                // Перевірте, чи точка знаходиться в межах всіх кол та точок перетину
                outsideCircles = true;

                for (int j = 0; j < NumAEC_; j++)
                {
                    int otherXAEC = XAEC[j];
                    int otherYAEC = YAEC[j];
                    int otherRadiusAEC = R_[j];

                    if (IsPointInCircle(x, y, otherXAEC, otherYAEC, otherRadiusAEC))
                    {
                        outsideCircles = false;
                        break;
                    }
                }


            } while (!outsideCircles);

            // Якщо точка поза межами всіх кол та кількість ще не досягла ліміту, то вивести її
           
                P_OP(x, y);
                builtObservationPointsCount++;
            
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
                MessageBox.Show("Не всі пункти контролю знаходяться на координатній площині");
            }
            else
            {
                Res();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
            button2.Enabled = true;
            button1.Enabled = false;

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


