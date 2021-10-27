using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierCurve
{
    class BesierDrawPoint
    {
        public const float distance = 15;

        private Point _p0, _p1, _p2;

        private static bool mouseDown;
        private static System.Drawing.Point lastLocation;
        public static void moveItselfWithMouse(BesierDrawPoint control,MouseEventHandler mouseEvent)
        {
            control.P1.AttachedButton.MouseDown += (o, e) => { mouseDown = true; lastLocation = e.Location; };
            control.P1.AttachedButton.MouseMove += mouseEvent;
            control.P1.AttachedButton.MouseMove += (o, e) =>
            {
                if (mouseDown)
                {
                    control.P1.X = (control.P1.X - lastLocation.X) + e.X;
                    control.P1.Y = (control.P1.Y - lastLocation.Y) + e.Y;
                    control.P2.X = 2 * control.point.X - control.P1.X;
                    control.P2.Y = 2 * control.point.Y - control.P1.Y;
                    control.P1.AttachedButton.Update();
                    control.P2.AttachedButton.Update();
                }
            };
            control.P2.AttachedButton.MouseUp += (o, e) => { mouseDown = false; };            
            
            control.P2.AttachedButton.MouseDown += (o, e) => { mouseDown = true; lastLocation = e.Location; };
            control.P2.AttachedButton.MouseMove += mouseEvent;
            control.P2.AttachedButton.MouseMove += (o, e) =>
            {
                if (mouseDown)
                {
                    control.P2.X = (control.P2.X - lastLocation.X) + e.X;
                    control.P2.Y = (control.P2.Y - lastLocation.Y) + e.Y;
                    control.P1.X = 2 * control.point.X - control.P2.X;
                    control.P1.Y = 2 * control.point.Y - control.P2.Y;
                    control.P1.AttachedButton.Update();
                    control.P2.AttachedButton.Update();
                }
            };
            control.P1.AttachedButton.MouseUp += (o, e) => { mouseDown = false; };
        }
        public Point P1
        {
            get
            {
                return _p1;
            }
        }
        public Point P2
        {
            get
            {
                return _p2;
            }
        }
        public Point point
        {
            get
            {
                return _p0;
            }
        }
        public BesierDrawPoint(float x,float y,Button point1,Button point2, MouseEventHandler mouseEvent)
        {
            _p0 = new Point(x, y,point1);
            _p1 = new Point(x, y + distance,point2);
            _p2 = new Point(x, y - distance,point2);
            moveItselfWithMouse(this,mouseEvent);
        }
        public static void DrawCurve(BesierDrawPoint p1, BesierDrawPoint p2,Graphics graphics,Pen curvePen, Pen linePen)
        {
            Point last = p1._p0;
            
            for (float t = 0; t < 1; t+=0.05f)
            {
                Point current = (1 - t) * (1 - t) * (1 - t) * p1._p0 +
                    3 * (1 - t) * (1 - t) * t * p1._p2 +
                    3 * (1 - t) * t * p2._p1 +
                    t * t * t * p2._p0;
                Point.Draw(current, last, graphics, curvePen);
                last = current;
            }
            Point.Draw(p1.P1, p1.P2,graphics,linePen);
            Point.Draw(p2.P1, p2.P2, graphics, linePen);
        }
    }
}
