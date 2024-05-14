using System;
using System.Numerics;


internal class Node
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public string Name { get; private set; }
    public Node? Parent { get; set; }
    public HashSet<Node> Children { get; set; } = [];

    public bool Pinned = false;

    public Node(string name, Vector2 position)
    {
        Position = position;
        Name = name;
    }

    public Node(string name)
    {
        Name = name;
    }
}
