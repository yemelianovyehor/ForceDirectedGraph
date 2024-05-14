using FDG.types;
using FDG.util;
using System.Numerics;
using System.Runtime.InteropServices;

namespace FDG
{
    public partial class Form1 : Form
    {
        ForceDirectedGraph FDGraph;

        const int UPDATE_MILISECONDS = 30;
        public Form1()
        {
            InitializeComponent();
            FDGraph = new(@"data\links.txt");
            FDGraph.Unfold(Size);

            DoubleBuffered = true;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = UPDATE_MILISECONDS;
            timer.Tick += CycleUpdate;
            timer.Start();
        }

        private void CycleUpdate(object sender, EventArgs e)
        {
            FDGraph.ArrangeStep();
            Console.WriteLine($"Timer Tick {e}");
            this.Invalidate();

        }

        void DrawGraph(PaintEventArgs e)
        {
            var brush = new SolidBrush(Color.Black);
            var pen = new Pen(Color.Red, 2);
            foreach (Node node in FDGraph.Nodes)
            {
                e.Graphics.FillEllipse(brush,
                    node.Position.X - FDGraph.NodeSize / 2, node.Position.Y - FDGraph.NodeSize / 2,
                    FDGraph.NodeSize, FDGraph.NodeSize);
            }
            foreach (Link spring in FDGraph.Links)
            {
                var start = spring.Start.Position;
                var end = spring.End.Position;
                Console.WriteLine($"Drawing {start}-{end}"); // !!!
                e.Graphics.DrawLine(pen, start.X, start.Y, end.X, end.Y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Paint += (sender, e) =>
            {
                DrawGraph(e);
            };

        }
    }
}
