using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BezierCurve
{
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
        public void Add(float x,float y,Button point1,Button point2,MouseEventHandler mouseEvent)
        {
            _besierPoints.Add(new BesierDrawPoint(x, y,point1,point2,mouseEvent));
        }
        public void Draw(Graphics graphics,Pen curvePen,Pen linePen)
        {
            for (int i = 0; i < _besierPoints.Count - 1; i++)
            {
                BesierDrawPoint.DrawCurve(_besierPoints[i], _besierPoints[i + 1], graphics,curvePen,linePen);
            }
        }
    }
}
