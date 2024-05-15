using System;
using System.Numerics;


internal class Node
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public string Name { get; private set; }
    public Node? Parent { get; set; }
    public HashSet<Node> Children { get; set; } = [];

    public bool Pinned = false;

    public Node(string name, Vector2 position) : this(name)
    {
        Position = position;
    }

    public Node(string name)
    {
        Name = name;
    }

    public void UpdatePosition(float minSpeed, float maxSpeed)
    {
        if (Pinned) return;

        if (Velocity.Length() > maxSpeed)
        {
            Velocity = Vector2.Normalize(Velocity) * 3;
        }
        else if (Position.Length() < minSpeed)
        {
            Velocity = Vector2.Zero;
        }
        if (float.IsNaN(Position.Length()))
        {
            //throw new Exception("Velocity contains NaN");

        }
        Position += Velocity;
        Velocity = Vector2.Zero;
    }
}
