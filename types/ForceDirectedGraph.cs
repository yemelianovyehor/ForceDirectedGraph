using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FDG.types
{
    internal class ForceDirectedGraph : Graph
    {
        const float ATTRACTION = 0.02F;
        const float REPULSION = 50F;
        //const float DRAG = 0.2f;
        const float REPULSION_RADIUS = 100;
        const float MAX_SPEED = 10;
        const float MIN_SPEED = 0.01f;

        public ForceDirectedGraph(string name) : base(name)
        {

        }

        Vector2 getAttractionVelocity(Link spring)
        {
            Vector2 vel = (spring.End.Position - spring.Start.Position) * ATTRACTION;
            //Console.WriteLine($"Attraction: vel:={vel}");
            //var magnitude = ATTRACTION * (vel.Length() - spring.Length);
            if (vel.Length() > MAX_SPEED)
            {
                return Vector2.Normalize(vel) * MAX_SPEED;
            }
            if (vel.Length() < MIN_SPEED)
            {
                return Vector2.Zero;
            }

            return vel;
        }

        Vector2 getRepulsionVelocity(Node node)
        {
            var vel = Vector2.Zero;
            foreach (Node neighbour in Nodes)
            {
                if (neighbour != node)
                {
                    var direction = (node.Position - neighbour.Position);
                    //Console.WriteLine($"{node.Name}~{neighbour.Name}: direction = {direction}");
                    if (direction.Length() < REPULSION_RADIUS)
                    {
                        var normalized = Vector2.Normalize(direction);
                        var force = 1 / direction.Length();
                        vel += normalized * (force == 0 ? 1 : force) * REPULSION;
                        //Console.WriteLine($"{node.Name}~{neighbour.Name}: vel = {vel} ; normalized = {normalized}");
                    }
                }
            }
            if (vel.Length() < MIN_SPEED)
            {
                vel = Vector2.Zero;
            }

            //Console.WriteLine($"{node.Name}: Repulsion = {vel}");
            return vel;
        }

        void UpdateVelocities(Link spring)
        {
            var attractionVelocity = getAttractionVelocity(spring).Round(2);
            spring.End.Velocity += -attractionVelocity;// + getRepulsionVelocity(spring.End).Round(2);
            spring.Start.Velocity += attractionVelocity;// + getRepulsionVelocity(spring.Start).Round(2);
            //Repulse(spring.End);
            //Console.WriteLine($"{spring}: Attraction = {attractionVelocity}");

        }

        public void ArrangeStep()
        {
            foreach (var spring in Links)
            {
                UpdateVelocities(spring);
            }
            foreach (var node in Nodes)
            {
                node.Velocity += getRepulsionVelocity(node).Round(2);
                node.UpdatePosition(MIN_SPEED, MAX_SPEED);
            }
        }


    }
}
