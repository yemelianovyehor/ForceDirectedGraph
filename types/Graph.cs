using FDG.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FDG.types
{
    internal class Graph
    {
        internal Link[] Links { get; set; } = [];
        internal HashSet<Node> Nodes { get; set; } = new HashSet<Node>();

        internal int NodeSize = 50;

        Node FindOrCreate(string name)
        {
            var node = Nodes.Where(i => i.Name == name).FirstOrDefault();
            if (node is default(Node))
            {
                return new Node(name);
            }
            return node;
        }

        public void Unfold(Size size)
        {
            var rnd = new Random();
            foreach (Node node in Nodes)
            {
                var x = rnd.Next(NodeSize / 2, size.Width - NodeSize);
                var y = rnd.Next(NodeSize / 2, (int)(size.Height - NodeSize * 1.5));
                node.Position = new Vector2(x, y);
            }
        }

        internal Graph(string url)
        {
            Dictionary<string, string[]> links = U.getLinks(url);
            foreach (var link in links)
            {
                var parent = FindOrCreate(link.Key);
                Nodes.Add(parent);
                foreach (var child in link.Value)
                {
                    var cNode = FindOrCreate(child);
                    Nodes.Add(cNode);
                    Links = [.. Links, new Link(parent, cNode)]; ;
                    Console.WriteLine($"Constructor: {Links}");
                }
            }
        }
        internal Graph(Size size, string url)
        {
            Dictionary<string, string[]> links = U.getLinks(url);
            foreach (var link in links)
            {
                var parent = new Node(link.Key);
                Nodes.Add(parent);
                foreach (var child in link.Value)
                {
                    var cNode = new Node(child);
                    Nodes.Add(cNode);
                    Links.Append(new Link(parent, cNode));
                }
            }
        }
    }
}
