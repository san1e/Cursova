using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public PictureBox[] PicOP;
        public PictureBox[] PicAEC;
        List<int> intersectingCircles = new List<int>();
        List<int> opInMultipleCirclesIndexes = new List<int>();

        int[] XAEC_;
        int[] YAEC_;
        int[] XOP_;
        int[] YOP_;
        int[] R_;
        int NumAEC_;
        int opInMultipleCirclesCount_ = 0;
        int builtObservationPointsCount = 0;

        public void P_Aec(int NumAEC, int[] XAEC, int[] YAEC)
        {
            PicAEC = new PictureBox[NumAEC];
            for (int i = 0; i < NumAEC; i++)
                {
                    PicAEC[i] = new PictureBox();
                    PicAEC[i].Image = Properties.Resources.AEC_;
                    PicAEC[i].Size = new Size(41, 41);
                    PicAEC[i].BackColor = Color.White;
                    PicAEC[i].SizeMode = PictureBoxSizeMode.Zoom;
                    PicAEC[i].Location = new Point(XAEC[i] - PicAEC[i].Width / 2, YAEC[i] - PicAEC[i].Height / 2);
                    Controls.Add(PicAEC[i]);
                
            }
            
        }

        
        public void P_Op(int NumOP, int[] XOP, int[] YOP)
        {
            PicOP = new PictureBox[NumOP];
            for (int i = 0; i < NumOP; i++)
            {
                PicOP[i] = new PictureBox();
                PicOP[i].Image = Properties.Resources.OP_;
                PicOP[i].Size = new Size(21, 21);
                PicOP[i].BackColor = Color.White;
                PicOP[i].SizeMode = PictureBoxSizeMode.Zoom;
                PicOP[i].Location = new Point(XOP[i] - PicOP[i].Width/2, YOP[i] - PicOP[i].Height/2);
                Controls.Add(PicOP[i]);
            }
        }
        public Form3(int[] R, int[] XAEC, int[] YAEC, int[] XOP, int[] YOP, int NumAEC, int NumOP)
        {
            InitializeComponent();
            R_ = R;
            XAEC_ = XAEC;
            YAEC_ = YAEC;
            XOP_ = XOP;
            YOP_ = YOP;
            NumAEC_ = NumAEC;

            P_Op(NumOP, XOP, YOP);
            P_Aec(NumAEC, XAEC, YAEC);

            Res();
        }
        public void Res()
        {
            int[] res1 = new int[NumAEC_];
            int opNotInAnyCircleCount = 0;
            int[] circlesContainingOP = new int[PicOP.Length];
            for (int i = 0; i < PicOP.Length; i++)
            {
                int xPicOP = PicOP[i].Location.X + PicOP[i].Width / 2;
                int yPicOP = PicOP[i].Location.Y + PicOP[i].Height / 2;
                Array.Clear(circlesContainingOP, 0, circlesContainingOP.Length);
                for (int j = 0; j < NumAEC_; j++)
                {
                    int xAEC = XAEC_[j];
                    int yAEC = YAEC_[j];
                    int radiusAEC = R_[j];
                    if (IsPointInCircle(xPicOP, yPicOP, xAEC, yAEC, radiusAEC))
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
            ResOutput(res1, opNotInAnyCircleCount, opInMultipleCirclesCount_);

        }
        private void ResOutput(int[] res1, int opNotInAnyCircleCount, int opInMultipleCirclesCount)
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
        static bool IsPointInCircle(int pointX, int pointY, int circleX, int circleY, int radius)
        {
            double distance = Math.Sqrt(Math.Pow(pointX - circleX, 2) + Math.Pow(pointY - circleY, 2));
            return distance <= radius;
        }

        private void P_OP_Out(int x, int y)
        {
            PictureBox PicOP = new PictureBox();
            PicOP.Image = Properties.Resources.OP_;
            PicOP.Size = new Size(21, 21);
            PicOP.BackColor = Color.White;
            PicOP.SizeMode = PictureBoxSizeMode.Zoom;
            PicOP.Location = new Point(x - PicOP.Width / 2, y - PicOP.Height / 2);
            Controls.Add(PicOP);
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
                int xAEC = (XAEC_[randomCircleIndex1] + XAEC_[randomCircleIndex2]) / 2;
                int yAEC = (YAEC_[randomCircleIndex1] + YAEC_[randomCircleIndex2]) / 2;
                int radiusAEC = (R_[randomCircleIndex1] + R_[randomCircleIndex2]) / 2;

                double angle = rnd.NextDouble() * 2 * Math.PI;
                double distance = rnd.Next(radiusAEC + 1, 2 * radiusAEC); // Виберіть випадковий радіус

                x = (int)(xAEC + distance * Math.Cos(angle));
                y = (int)(yAEC + distance * Math.Sin(angle));

                // Округлення координат до 50
                x = RoundToNearest(x, 50);
                y = RoundToNearest(y, 50);
                if (x < 0)
                {
                    x *= -1;
                }
                else if (y < 0)
                {
                    y *= -1;
                }
                else if (x == 0)
                {
                    x += rnd.Next(1, 15) * 50;
                }
                else if (y == 0)
                {
                    y += rnd.Next(1, 15) * 50;

                }
                else if (x > 750)
                {
                    x = 750;
                }
                else if (y > 750)
                {
                    y = 750;
                }

                // Перевірте, чи точка знаходиться в межах всіх кол та точок перетину
                outsideCircles = true;

                for (int j = 0; j < NumAEC_; j++)
                {
                    int otherXAEC = XAEC_[j];
                    int otherYAEC = YAEC_[j];
                    int otherRadiusAEC = R_[j];

                    if (IsPointInCircle(x, y, otherXAEC, otherYAEC, otherRadiusAEC))
                    {
                        outsideCircles = false;
                        break;
                    }
                }


            } while (!outsideCircles);

            // Якщо точка поза межами всіх кол та кількість ще не досягла ліміту, то вивести її

            P_OP_Out(x, y);
            builtObservationPointsCount++;

        }
        private int RoundToNearest(int value, int roundTo)
        {
            return (int)(Math.Round((double)value / roundTo) * roundTo);
        }

    
        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
           
            for (int i = 0; i < NumAEC_; i++)
            {
                int x = XAEC_[i] - (R_[i]);
                int y = YAEC_[i] - (R_[i]);
                int R = R_[i];

                Rectangle clipRect = new Rectangle(0, 0, 750, 750);
                g.SetClip(clipRect);
                g.DrawArc(new Pen(Color.Black, 5), x, y, R*2, R*2, 0, 360);
                g.ResetClip();
            }

            for (int i = 50; i <= 750; i +=50)
            {
                g.DrawLine(new Pen(Color.Black), i, 0, i, 750);
                g.DrawLine(new Pen(Color.Black), 0, i, 750, i);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OutSideOP();
        }
    }
}
