using System.Collections.Generic;
using System.Drawing;

using System.Windows.Forms;

namespace BezierCurve
{
    public partial class Form1 : Form
    {
        private List<Button> _buttons;
        private CompositeBesier _curve;
        private Graphics _graphics;
        private Pen _curvePen;
        private Pen _linePen;
        public Form1()
        {
            InitializeComponent();
            _buttons = new List<Button>();
            _curve = new CompositeBesier();
            _graphics = CreateGraphics();
            _curvePen = new Pen(Color.Black, 2);
            _linePen = new Pen(Color.Red, 1);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
            //nbtn.Text = Point.Text;
            _curve.Add(e.X, e.Y,Point,Point2,(o,ev)=> { 
                _graphics.Clear(Color.White);
                _curve.Draw(_graphics,_curvePen,_linePen);
            });
            _graphics.Clear(Color.White);
            _curve.Draw(_graphics,_curvePen,_linePen);
        }

    }
}
