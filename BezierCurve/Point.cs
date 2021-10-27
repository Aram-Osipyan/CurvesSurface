using System.Drawing;
using System.Windows.Forms;

namespace BezierCurve
{
    class Point
    {
        private Button _attachedButton;
        public Button AttachedButton
        {
            get
            {
                return _attachedButton;
            }
        }
        public float X 
        {
            get
            {
                return _attachedButton.Location.X;
            }
            set
            {
                _attachedButton.Location 
                    = new System.Drawing.Point((int)value, _attachedButton.Location.Y);
            }
        }
        public float Y
        {
            get
            {
                return _attachedButton.Location.Y;
            }
            set
            {
                _attachedButton.Location
                    = new System.Drawing.Point(_attachedButton.Location.X, (int)value);
            }
        }
        public Point(float x,float y,Button point)
        {
            _attachedButton = point.Clone();
            //ControlExtension.Draggable(_attachedButton, true);
            X = x;
            Y = y;
        }
        public Point()
        {
            _attachedButton = new Button();
        }
        public static Point operator+(Point p1,Point p2)
        {
            Point p = new Point();
            p.X = p1.X + p2.X;
            p.Y = p1.Y + p2.Y;
            return p;
        }
        public static Point operator -(Point p1, Point p2)
        {
            Point p = new Point();
            p.X = p1.X - p2.X;
            p.Y = p1.Y - p2.Y;
            return p;
        }
        public static Point operator *( Point p1, float t)
        {
            Point p = new Point();
            p.X = p1.X * t;
            p.Y = p1.Y * t;
            return p;
        }
        public static Point operator *(float t, Point p1)
        {
            Point p = new Point();
            p.X = p1.X * t;
            p.Y = p1.Y * t;
            return p;
        }
        public static Point Ratio(Point p1, Point p2,float t)
        {
            Point p = p1 + t * (p2 - p1);
            return p;
        }
        public static void Draw(Point p1, Point p2,Graphics graphics,Pen pen)
        {
            graphics.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);

        }
    }
}
