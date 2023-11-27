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
        public Label[] L;
        Graphics g;
        int[] XAEC_;
        int[] YAEC_;
        int[] R_;
        int[] XOP_;
        int[] YOP_;
        int NumAEC_;
        int NumOP_;
        public void P_Aec(int NumAEC, int[] XAEC, int[] YAEC, int[] R)
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
            P_Aec(NumAEC, XAEC, YAEC, R);

            int count = 0;

            int[] res1 = new int[NumAEC];


            //for (int i = 0; i < NumAEC; i++)
            //{
            //    int cx = XAEC_[i] - (R_[i]);
            //    int cy = YAEC_[i] - (R_[i]);
            //    for (int j = 0; j < NumOP; j++)
            //    {
            //        int x = XOP[j] - PicOP[j].Width / 2;
            //        int y = YOP[j] - PicOP[j].Height / 2;
            //        double dis = Math.Sqrt(Math.Pow(x - cx, 2) + Math.Pow(y - cy, 2));

            //        if (dis <= R_[i])
            //        {
            //            count++;    
            //        }
            //    }
            //}

           

        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
           
            for (int i = 0; i < NumAEC_; i++)
            {
                int x = XAEC_[i] - (R_[i]);
                int y = YAEC_[i] - (R_[i]);
                int R = R_[i];
                g.DrawEllipse(new Pen(Color.Black,5), x, y, R*2, R*2);
                g.FillEllipse(new SolidBrush(Color.Red), x,y, R*2, R*2);
            }
            int count = 0;
            int[] res1 = new int[NumAEC_];

            // Проверка попадания PicOP в круг
            for (int i = 0; i < PicOP.Length; i++)
            {
                int xPicOP = PicOP[i].Location.X + PicOP[i].Width / 2;
                int yPicOP = PicOP[i].Location.Y + PicOP[i].Height / 2;
                int n = 10;
                for (int j = 0; j < NumAEC_; j++)
                {
                    int xAEC = XAEC_[j];
                    int yAEC = YAEC_[j];
                    int radiusAEC = R_[j];

                    double distance = Math.Sqrt(Math.Pow(xPicOP - xAEC, 2) + Math.Pow(yPicOP - yAEC, 2));

                    if (distance <= radiusAEC)
                    {
                        res1[j]++;
                    }
                }
            }

            Label[] labels = new Label[NumAEC_];
            for (int i = 0; i < NumAEC_; i++)
            {
                labels[i] = new Label();
                labels[i].Location = new Point(800, 10 + i * 20);
                labels[i].Text = $"Количество пс, попавших в АЕС {i + 1}: {res1[i]}";
                labels[i].AutoSize = true;
                Controls.Add(labels[i]);
            }

            for (int i = 50; i <= 750; i +=50)
            {
                g.DrawLine(new Pen(Color.Black), i, 0, i, 750);
                g.DrawLine(new Pen(Color.Black), 0, i, 750, i);
            }
        }

       
        
    }
}
