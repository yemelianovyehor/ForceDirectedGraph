using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FDG.types
{
    internal class ForceDirectedGraph : Graph
    {
        const float ATTRACTION = 0.01F;
        const float REPULSION = 0.05F;
        const float DRAG = 0.5f;
        const float REPULSION_RADIUS = 50;
        const float MAX_SPEED = 5;
        const float MIN_SPEED = 1;

        public ForceDirectedGraph(string name) : base(name)
        {

        }

        Vector2 Attract(Link spring)
        {
            Vector2 vel = (spring.End.Position - spring.Start.Position) * DRAG;
            var magnitude = ATTRACTION * (vel.Length() - spring.Length);
            if (vel.Length() > MAX_SPEED)
            {
                vel = Vector2.Normalize(vel) * MAX_SPEED;
            }
            if (vel.Length() < MIN_SPEED)
            {
                vel = Vector2.Zero;
            }

            return Vector2.Normalize(vel) * magnitude;
        }

        Vector2 Repulse(Node node)
        {
            var vel = Vector2.Zero;
            foreach (Node neighbour in Nodes)
            {
                if (neighbour != node)
                {
                    var direction = (node.Position - neighbour.Position);
                    if (direction.Length() < REPULSION_RADIUS)
                    {
                        vel -= direction * REPULSION;
                    }
                }
            }
            vel = new Vector2(MathF.Round(vel.X,2), MathF.Round(vel.Y,2));
            return vel;
        }

        void MoveNodes(Link spring)
        {
            var attractionVelocity = Attract(spring);
            var repulsionVelosityStart = Repulse(spring.Start);
            var repulsionVelosityEnd = Repulse(spring.End);
            spring.End.Position += attractionVelocity - repulsionVelosityEnd;
            spring.Start.Position -= attractionVelocity + repulsionVelosityStart;

        }

        public void ArrangeStep()
        {
            foreach (var spring in Links)
            {
                MoveNodes(spring);
            }
        }


    }
}
