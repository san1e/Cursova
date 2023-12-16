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
        int[] XAEC_;
        int[] YAEC_;
        int[] R_;
        int[] XOP_;
        int[] YOP_;
        int NumAEC_;
        int NumOP_;
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
            this.R_ = R;
            this.XAEC_ = XAEC;
            this.YAEC_ = YAEC;
            this.NumAEC_ = NumAEC;
            this.XOP_= XOP;
            this.YOP_= YOP;
            this.NumOP_ = NumOP;

            P_Op(NumOP, XOP, YOP);
            P_Aec(NumAEC, XAEC, YAEC);
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        static bool IsPointInCircle(int pointX, int pointY, int circleX, int circleY, int radius)
        {
            double distance = Math.Sqrt(Math.Pow(pointX - circleX, 2) + Math.Pow(pointY - circleY, 2));
            return distance <= radius;
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
            int[] res1 = new int[NumAEC_];
            int opNotInAnyCircleCount = 0;
            int opInMultipleCirclesCount = 0;
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
                    }

                }
                if (circlesContainingOP[i] == 0)
                {
                    opNotInAnyCircleCount++;
                }

                if (circlesContainingOP[i] > 1)
                {
                    opInMultipleCirclesCount++;
                }

            }



            Label[] labels = new Label[NumAEC_];
            for (int i = 0; i < NumAEC_; i++)
            {
                labels[i] = new Label();
                labels[i].Location = new Point(800, 40 + i * 20);
                labels[i].Text = $"Кількість пунктів спостереження, які потрапили в радіус АЕС {i + 1}: {res1[i]}";
                labels[i].AutoSize = true;
                Controls.Add(labels[i]);
            }

            Label l = new Label();
            l.Location = new Point(800, 20);
            l.Text = $"Кількість пунктів спостереження, які не використовуються: {opNotInAnyCircleCount}";
            l.AutoSize = true;
            Controls.Add(l);

            Label l1 = new Label();
            l1.Location = new Point(800, 0);
            l1.Text = $"Кількість пунктів спостереження, які знаходяться на перетені: {opInMultipleCirclesCount}";
            l1.AutoSize = true;
            Controls.Add(l1);

            for (int i = 50; i <= 750; i +=50)
            {
                g.DrawLine(new Pen(Color.Black), i, 0, i, 750);
                g.DrawLine(new Pen(Color.Black), 0, i, 750, i);
            }
        }

       
        
    }
}
