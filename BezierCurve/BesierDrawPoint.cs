using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierCurve
{
    class Point
    {
        private Button _attachedButton;
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
    class BesierDrawPoint
    {
        public const float distance = 15;

        private Point _p0, _p1, _p2;
        public Point P1
        {
            get
            {
                return _p1;
            }
            set
            {
                _p1 = value;
                _p2 = 2 * _p0 - value;
            }
        }
        public Point P2
        {
            get
            {
                return _p2;
            }
            set
            {
                _p2 = value;
                _p1 = 2 * _p0 - value;
            }
        }
        public Point point
        {
            get
            {
                return _p0;
            }
        }
        public BesierDrawPoint(float x,float y,Button point1,Button point2)
        {
            _p0 = new Point(x, y,point1);
            _p1 = new Point(x, y + distance,point2);
            _p2 = new Point(x, y - distance,point2);
        }
        public static void DrawCurve(BesierDrawPoint p1, BesierDrawPoint p2,Graphics graphics,Pen pen)
        {
            Point last = p1._p0;
            
            for (float t = 0; t < 1; t+=0.05f)
            {
                Point current = (1 - t) * (1 - t) * (1 - t) * p1._p0 +
                    3 * (1 - t) * (1 - t) * t * p1._p2 +
                    3 * (1 - t) * t * p2._p1 +
                    t * t * t * p2._p0;
                Point.Draw(current, last, graphics, pen);
                last = current;
            }

        }
    }
    class CompositeBesier
    {
        private List<BesierDrawPoint> _besierPoints;
        public CompositeBesier()
        {
            _besierPoints = new List<BesierDrawPoint>();
        }
        /// <summary>
        /// Add point to curve
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Add(float x,float y,Button point1,Button point2)
        {
            _besierPoints.Add(new BesierDrawPoint(x, y,point1,point2));
        }
        public void Draw(Graphics graphics,Pen pen)
        {
            for (int i = 0; i < _besierPoints.Count - 1; i++)
            {
                BesierDrawPoint.DrawCurve(_besierPoints[i], _besierPoints[i + 1], graphics,pen);
            }
        }
    }
}
