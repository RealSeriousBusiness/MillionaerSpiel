using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Millionär
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            new Main(this).ShowDialog();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            drawText(e.Graphics, label1);
            drawText(e.Graphics, label2);
            draw(e.Graphics, pictureBox1);
        }

        private void draw(Graphics g, PictureBox todraw)
        {
            g.DrawImage(resize(todraw.Image, todraw.Size), todraw.Location);
        }

        private void drawText(Graphics g, Label todraw)
        {
            g.DrawString(todraw.Text, todraw.Font, new SolidBrush(todraw.ForeColor), todraw.Location);
        }

        Bitmap map;
        private Bitmap resize(Image resize, Size p)
        {
            map = new Bitmap((Bitmap)resize, p.Width, p.Height);
            return map;
        }

    }
}
