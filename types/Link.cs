using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FDG.types
{
    internal class Link
    {
        public Node Start { get; private set; }
        public Node End { get; private set; }

        public float Length
        {
            get
            {
                return (Start.Position - End.Position).Length();
            }

        }
        public Link(Node startNode, Node endNode)
        {
            Start = startNode;
            End = endNode;
            End.Parent = Start;
            Start.Children.Add(End);
        }
    }
}
