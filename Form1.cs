using FDG.types;
using FDG.util;
using System.Numerics;
using System.Runtime.InteropServices;

namespace FDG
{
    public partial class Form1 : Form
    {
        Graph FDGraph;
        public Form1()
        {
            InitializeComponent();
            FDGraph = new Graph(@"data\links.txt");
            FDGraph.Unfold(Size);
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
            foreach (Link link in FDGraph.Links)
            {
                var start = link.Start.Position;
                var end = link.End.Position;
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
