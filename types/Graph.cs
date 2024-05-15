using FDG.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private const int NODE_SIZE = 30;
        const int UNFOLD_MAX_DISTANCE = 200;
        const float RNG_ANGLE_RANGE = (float)(Math.PI / 180) * 120;

        public int NodeSize { get { return NODE_SIZE; } }

        Node FindOrCreate(string name)
        {
            var node = Nodes.Where(i => i.Name == name).FirstOrDefault();
            if (node is default(Node))
            {
                return new Node(name);
            }
            return node;
        }

        Vector2 getRelativeDirection(Node node)
        {
            if (node.Parent == null)
            {
                throw new Exception($"Node {node.Name} has no Parent");
            }
            var direction = node.Position - node.Parent.Position;
            //var normalized = Vector2.Normalize(direction);
            return direction;
        }
        /// <summary>
        /// tilts Vector2 by random angle in range(-radians/2 , radians/2)
        /// </summary>
        /// <param name="direction">Vector to tilt</param>
        /// <param name="angle"> </param>
        ///// <returns></returns>
        Vector2 tiltDirection(Vector2 direction, float radians)
        {
            var rng = new Random();
            //float angle = rng.Next(-(int)(radians*50), (int)(radians * 50)) / 100f;
            float angle = (float)rng.NextSingle() * radians / 2 - radians / 2;
            //angle += MathF.Atan2(0 - direction.Y, 1 - direction.X);
            //var tiltedDir = direction + new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            var x = direction.X * MathF.Cos(angle) - direction.Y * MathF.Sin(angle);
            var y = direction.X * MathF.Sin(angle) + direction.Y * MathF.Cos(angle);
            var tiltedDir = new Vector2(x, y);
            return tiltedDir;
            //return direction;
        }

        public void Unfold(Size size)
        {
            foreach (Node node in Nodes)
            {
                if (node.Parent?.Parent != null)
                {
                    var direction = getRelativeDirection(node.Parent);
                    //var normalized = Vector2.Normalize(direction);
                    node.Position = node.Parent.Position + tiltDirection(direction, RNG_ANGLE_RANGE);
                }
                else if (node.Parent != null)
                {
                    var rnd = new Random();
                    node.Position = node.Parent.Position +
                        new Vector2(rnd.Next(-UNFOLD_MAX_DISTANCE, UNFOLD_MAX_DISTANCE),
                                    rnd.Next(-UNFOLD_MAX_DISTANCE, UNFOLD_MAX_DISTANCE));
                }
                else
                {
                    //var rnd = new Random();
                    //var x = rnd.Next(NodeSize / 2, size.Width - NodeSize);
                    //var y = rnd.Next(NodeSize / 2, (int)(size.Height - NodeSize * 1.5));
                    //node.Position = new Vector2(x, y);
                    node.Position = new Vector2(size.Width / 2, size.Height / 2);
                    node.Pinned = true;
                }
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
                    Links = [.. Links, new Link(parent, cNode)];
                    //Console.WriteLine($"Constructor: {Links}");
                }
            }
        }
    }
}
