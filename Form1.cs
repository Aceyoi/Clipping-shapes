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
        private List<Point> userPoints = new List<Point>(); 
        private Bitmap drawingBitmap;
        private Graphics graphics; 

        public Form1()
        {
            InitializeComponent();
            drawingBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(drawingBitmap);
            pictureBox1.Image = drawingBitmap;
            pictureBox1.MouseClick += new MouseEventHandler(PictureBox1_MouseClick);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearCanvas();
            SolidBrush brush = new SolidBrush(Color.Red);
            graphics.FillRectangle(brush, 50, 50, 500, 300);
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearCanvas();
            SolidBrush brush = new SolidBrush(Color.Red);
            graphics.FillEllipse(brush, 25, 25, 350, 350);
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearCanvas();
            SolidBrush brush = new SolidBrush(Color.Red);
            Point[] trianglePoints = {
                new Point(100, 50),
                new Point(0, 250),
                new Point(200, 250)
            };
            graphics.FillPolygon(brush, trianglePoints);
            pictureBox1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (userPoints.Count < 2)
            {
                MessageBox.Show("Недостаточно точек для создания области отсечения.");
                return;
            }

            GraphicsPath path = new GraphicsPath();
            path.AddLines(userPoints.ToArray());
            path.AddLine(userPoints[userPoints.Count - 1], userPoints[0]); 

            Region clipRegion = new Region(path);

            graphics.SetClip(clipRegion, CombineMode.Intersect);

            graphics.Clear(Color.White);

            if (button1.Focused)
            {
                SolidBrush brush = new SolidBrush(Color.Red);
                graphics.FillRectangle(brush, 50, 50, 500, 300);
            }
            else if (button2.Focused)
            {
                SolidBrush brush = new SolidBrush(Color.Red);
                graphics.FillEllipse(brush, 25, 25, 350, 350);
            }
            else if (button3.Focused)
            {
                SolidBrush brush = new SolidBrush(Color.Red);
                Point[] trianglePoints = {
                    new Point(100, 50),
                    new Point(0, 250),
                    new Point(200, 250)
                };
                graphics.FillPolygon(brush, trianglePoints);
            }

            graphics.ResetClip();

            pictureBox1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearCanvas();
            userPoints.Clear();
            isFirstClick = true;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFirstClick)
            {
                previousPoint = e.Location;
                userPoints.Add(previousPoint);
                isFirstClick = false;
            }
            else
            {
                userPoints.Add(e.Location);
                graphics.DrawLine(Pens.Black, previousPoint, e.Location);
                previousPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void ClearCanvas()
        {
            graphics.Clear(Color.White);
            pictureBox1.Invalidate();
        }
    }
}