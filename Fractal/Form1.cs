using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        public void ReadRules()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"Rules.txt");
            string order = textBox1.Text;
            if (!String.IsNullOrEmpty(order))
            {
                int ord = int.Parse(order);
                LSystem lsys = new LSystem(lines, ord, pictureBox1.Width, pictureBox1.Height);
                Random r = new Random();
                lsys.DrawFractal(bmp, r.NextDouble());
                pictureBox1.Image = bmp;
            }
            else
            {
                MessageBox.Show("You didn't enter some values!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"Rules.txt");
            string order = textBox1.Text;
            if (!String.IsNullOrEmpty(order))
            {
                int ord = int.Parse(order);
                LSystem lsys = new LSystem(lines, ord, pictureBox1.Width, pictureBox1.Height);
                lsys.DrawFractal(bmp);
                pictureBox1.Image = bmp;
            }
            else
            {
                MessageBox.Show("You didn't enter some values!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"tree.txt");
            LSystem lsys = new LSystem(lines, 11, pictureBox1.Width, pictureBox1.Height);
            lsys.DrawTree(bmp);
            pictureBox1.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
        }
    }

    public class LSystem
    {
        string atom;
        int angle;
        int startDirection;
        int order;
        int width;
        int height;
        string permutation;
        PointF pointLeft = new PointF(float.MaxValue, 0);
        PointF pointRight = new PointF(float.MinValue, 0);
        PointF pointHigh = new PointF(0, float.MaxValue);
        PointF pointLow = new PointF(0, float.MinValue);

        Dictionary<char, string> rules = new Dictionary<char, string>();
        LinkedList<PointFWrapper> points = new LinkedList<PointFWrapper>();


        public LSystem(string[] lines, int _order, int _width, int _height)
        {
            order = _order;
            width = _width;
            height = _height;
            string[] first = lines[0].Split();
            atom = first[0];
            angle = int.Parse(first[1]);
            startDirection = int.Parse(first[2]);
            for (int i = 1; i < lines.Length; ++i)
            {
                rules.Add(lines[i][0], lines[i].Substring(3));
            }
            Permutate(order);
        }

        public void Permutate(int n)
        {
            string result = atom;
            char[] keys = rules.Keys.ToArray();
            for (int i = 0; i < n; ++i)
            {
                int currRule = 0;
                currRule = result.IndexOfAny(keys);
                while (currRule != -1)
                {
                    char key = result[currRule];
                    string value = rules[key];
                    result = result.Remove(currRule, 1);
                    result = result.Insert(currRule, value);
                    currRule += value.Length;
                    currRule = result.IndexOfAny(keys, currRule);
                }
            }
            permutation = result;
        }

        public class Edge
        {
            public PointFWrapper firstPoint { get; set; }
            public PointFWrapper secondPoint { get; set; }
            public Color color { get; set; }
            public float thick { get; set; }

            public Edge(PointFWrapper _first, PointFWrapper _second)
            {
                firstPoint = _first;
                secondPoint = _second;
            }
        }

        public class PointFWrapper
        {
            public PointF point;
            public PointFWrapper(PointF p)
            {
                point = p;
            }
        }

        private void ShiftBounds(PointF point)
        {
            if (point.X < pointLeft.X)
                pointLeft = point;
            else if (point.X > pointRight.X)
                pointRight = point;
            if (point.Y > pointLow.Y)
                pointLow = point;
            else if (point.Y < pointHigh.Y)
                pointHigh = point;
        }

        private void Iterate(ref LinkedListNode<PointFWrapper> current, List<Edge> edges, int rotationAngle, Color color, int thickness = -1)
        {
            LinkedListNode<PointFWrapper> last = current;
            double tempAngle = Math.PI * rotationAngle / 180.0;
            PointF p = new PointF((float)Math.Cos(tempAngle) + last.Value.point.X, (float)Math.Sin(tempAngle) + last.Value.point.Y);
            ShiftBounds(p);
            points.AddLast(new PointFWrapper(p));
            current = points.Last;
            edges.Add(new Edge(last.Value, current.Value));
            if (thickness != -1)
            {
                edges[edges.Count - 1].color = color;
                edges[edges.Count - 1].thick = thickness;
            }
        }

        public double[,] MultiplyMatrix(double[,] a, double[,] b)
        {
            int aN = a.GetLength(0);
            int aM = a.GetLength(1);
            int bN = b.GetLength(0);
            int bM = b.GetLength(1);
            if (aM != bN)
            {
                return null;
            }

            double[,] c = new double[aN, bM];

            for (int i = 0; i < aN; i++)
            {
                for (int j = 0; j < bM; j++)
                {
                    double sum = 0;
                    for (int m = 0; m < aM; m++)
                    {
                        sum += a[i, m] * b[m, j];
                    }
                    c[i, j] = sum;
                }
            }
            return c;
        }

        public PointF ScalePoint(double x, double y, double dx, double dy, double alpha, double beta, PointF p)
        {
            double[,] scaleMatrix = {
                { alpha, 0, 0 },
                { 0, beta, 0 },
                { (1-alpha)*p.X+dx,(1-beta)*p.Y +dy, 1 }
            };

            double[,] point = { { x, y, 1 } };
            var c = MultiplyMatrix(point, scaleMatrix);
            return new PointF((float)c[0, 0], (float)c[0, 1]);
        }

        public void DrawFractal(Bitmap bmp, double rotRand = 1)
        {
            PointF point = new PointF(width / 2, height / 2);
            ShiftBounds(point);
            int rotationAngle = startDirection;
            Stack<(LinkedListNode<PointFWrapper>, int)> branchesStack = new Stack<(LinkedListNode<PointFWrapper>, int)>();
            points.AddLast(new PointFWrapper(point));
            LinkedListNode<PointFWrapper> current = points.First;
            branchesStack.Push((current, rotationAngle));
            List<Edge> edges = new List<Edge>();

            foreach (var key in permutation)
            {
                switch (key)
                {
                    case 'F': Iterate(ref current, edges, rotationAngle, Color.Black);
                        break;
                    case '+': rotationAngle -= (int)(angle * rotRand);
                        break;
                    case '-': rotationAngle += (int)(angle * rotRand);
                        break;
                    case '[': branchesStack.Push((current, rotationAngle));
                        break;
                    case ']': var p_angle = branchesStack.Pop(); (current, rotationAngle) = (p_angle.Item1, p_angle.Item2); 
                        break;
                    default: break;
                }
                if (rotRand != 1.0)
                {
                    Random r = new Random();
                    rotRand = r.NextDouble();
                }
            }
            
            float dx = pointRight.X - pointLeft.X;
            float dy = pointLow.Y - pointHigh.Y;
            PointF center = new PointF(pointLeft.X + dx / 2, pointHigh.Y + dy / 2);

            float resizeCoeff = Math.Min(width / dx, height / dy);
            current = points.First;
            while (current != null)
            {
                current.Value.point = ScalePoint(current.Value.point.X, current.Value.point.Y, width / 2 - center.X, height / 2 - center.Y, resizeCoeff, resizeCoeff, center);
                current = current.Next;
            }
            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (var edge in edges)
                {
                    g.DrawLine(new Pen(Color.Black, 0.1f), edge.firstPoint.point, edge.secondPoint.point);
                }
            }
        }

        public void DrawTree(Bitmap bmp)
        {
            PointF point = new PointF(width / 2, height / 2);
            Random r = new Random();
            double rotRand = r.NextDouble();
            ShiftBounds(point);
            int rotationAngle = startDirection;
            Stack<(LinkedListNode<PointFWrapper>, int, int, Color)> branchesStack = new Stack<(LinkedListNode<PointFWrapper>, int, int, Color)>();
            points.AddLast(new PointFWrapper(point));
            LinkedListNode<PointFWrapper> current = points.First;
            List<Edge> edges = new List<Edge>();
            int thick = order;
            Color col = Color.FromArgb(23, 16, 9);

            foreach (var key in permutation)
            {
                switch (key)
                {
                    case 'F':
                        Iterate(ref current, edges, rotationAngle, col, thick);
                        break;
                    case 'X':
                        Iterate(ref current, edges, rotationAngle, col, thick);
                        break;
                    case '+':
                        rotationAngle -= (int)(angle * rotRand);
                        break;
                    case '-':
                        rotationAngle += (int)(angle * rotRand);
                        break;
                    case '[':
                        branchesStack.Push((current, rotationAngle, thick, col));
                        break;
                    case ']':
                        var p_angle = branchesStack.Pop(); (current, rotationAngle, thick, col) = 
                            (p_angle.Item1, p_angle.Item2, p_angle.Item3, p_angle.Item4);
                        break;
                    case '@':
                        thick--;
                        col = Color.FromArgb(col.R - 1 > 0 ? col.R - 1 : 0, col.G + 5, col.B - 1 > 0 ? col.B - 1 : 0);
                        break;
                    default: break;
                }
                rotRand = r.NextDouble();
            }

            float dx = pointRight.X - pointLeft.X;
            float dy = pointLow.Y - pointHigh.Y;
            PointF center = new PointF(pointLeft.X + dx / 2, pointHigh.Y + dy / 2);

            float resizeCoeff = Math.Min(width / dx, height / dy);
            current = points.First;
            while (current != null)
            {
                current.Value.point = ScalePoint(current.Value.point.X, current.Value.point.Y, width / 2 - center.X, height / 2 - center.Y, resizeCoeff, resizeCoeff, center);
                current = current.Next;
            }
            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (var edge in edges)
                {
                    g.DrawLine(new Pen(edge.color, edge.thick), edge.firstPoint.point, edge.secondPoint.point);
                }
            }
        }
    }
}
