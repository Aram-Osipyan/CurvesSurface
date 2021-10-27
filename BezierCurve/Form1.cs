using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace BezierCurve
{
    public partial class Form1 : Form
    {
        private List<Button> _buttons;
        private CompositeBesier _curve;
        private Graphics _graphics;
        private Pen _curvePen;
        public Form1()
        {
            InitializeComponent();
            _buttons = new List<Button>();
            _curve = new CompositeBesier();
            _graphics = CreateGraphics();
            _curvePen = Pens.Black;
            ControlExtension.Draggable(Point, true);
            ControlExtension.Draggable(Point2, true);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //nbtn.Text = Point.Text;
            _curve.Add(e.X, e.Y,Point,Point2);
            _graphics.Clear(Color.White);
            _curve.Draw(_graphics,_curvePen);
        }
    }
}
