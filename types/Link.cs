using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDG.types
{
    internal struct Link
    {
        public Node Start { get; private set; }
        public Node End { get; private set; }

        public Link(Node startNode, Node endNode)
        {
            Start = startNode;
            End = endNode;
            End.Parent = Start;
            Start.Children.Add(End);
        }
    }
}
