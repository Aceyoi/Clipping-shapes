using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        private Point previousPoint;
        private bool isFirstClick = true;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseClick += new MouseEventHandler(PictureBox1_MouseClick);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            SolidBrush brush = new SolidBrush(Color.Red);
            graphics.FillRectangle(brush, 50, 50, 500, 300);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            SolidBrush brush = new SolidBrush(Color.Red);
            graphics.FillEllipse(brush, 25, 25, 350, 350);            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            SolidBrush brush = new SolidBrush(Color.Red);
            Point[] trianglePoints = {
            new Point(100, 50),
            new Point(0, 250),
            new Point(200, 250)
            };
            graphics.FillPolygon(brush, trianglePoints);           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFirstClick)
            {
                previousPoint = e.Location;
                isFirstClick = false;
            }
            else
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    g.DrawLine(Pens.Black, previousPoint, e.Location);
                }

                previousPoint = e.Location;
            }
        }

    }
}