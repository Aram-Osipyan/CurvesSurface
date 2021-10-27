using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            pictureBox1.BackColor = Color.White;
        }

        private void midpoint(int hL_y, int hL_x, int hR_y, int hR_x,int iteration)
        {
            if(iteration > 0)
            {
                --iteration;
                double len = Math.Sqrt((hR_x - hL_x) * (hR_x - hL_x) + (hR_y - hL_y) * (hR_y - hL_y));
                Random rand = new Random();
                double h = (hL_y + hR_y) / 2.0;
                double h2 = hL_y - h;
                double len2 = len / 2.0;
                int x = hL_x + (int)Math.Round(Math.Sqrt(len2 * len2 - h2 * h2));
                int random = rand.Next((int)((-1) *  float.Parse(textBox4.Text) * (int)Math.Round(len)), 
                                       (int)(float.Parse(textBox4.Text) * (int)Math.Round(len)));
                h  = h + random;
                if (h < 0)
                    h = 10;
                if (h > pictureBox1.Height)
                    h = pictureBox1.Height - 10;
                
                Pen p1 = new Pen(Color.White, 2);
                g.DrawLine(p1, hL_x, hL_y, hR_x, hR_y);
                g.DrawLine(p, hL_x, hL_y, x, (int)Math.Ceiling(h));
                g.DrawLine(p, x, (int)Math.Ceiling(h), hR_x, hR_y);
                pictureBox1.Refresh();
                midpoint(hL_y, hL_x, (int)Math.Ceiling(h), x, iteration);
                midpoint((int)Math.Ceiling(h), x, hR_y, hR_x, iteration);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            button2_Click(sender, e);
            int iterations = int.Parse(textBox3.Text); 
            p = new Pen(Color.Black, 1);
            g.DrawLine(p, 0,int.Parse(textBox1.Text) , pictureBox1.Width, int.Parse(textBox2.Text));
            pictureBox1.Refresh();
            midpoint(int.Parse(textBox1.Text), 0 , int.Parse(textBox2.Text), pictureBox1.Width, iterations);
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if(!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
    }
}
