using Common;
using SmartWorld.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    internal class EyeManager
    {
        private double EyeDepth { get; set; }
        private Agent Agent { get; set; }
        private World World { get; set; }
        private Vector EyeStart { get; set; }
        private Vector EyeEnd { get; set; }

        public EyeManager(World world, Agent agent, double eyeAngle)
        {
            EyeDepth = ConfigManager.Current.AgentEyeDepth;
            Agent = agent;
            World = world;

            var eyeVector = agent.LookAt.Rotated(eyeAngle);
            EyeStart = agent.Position;
            EyeEnd = EyeStart + eyeVector * EyeDepth;
        }

        public Color See()
        {
            var result = Color.None;

            // Check if we are hitting other elements
            var otherAgents = World.Agents.Except(new[] { Agent });
            var allElements = otherAgents.OfType<IElement>().Union(World.FoodElements.OfType<IElement>());
            var allElementsSorted = allElements.OrderBy(o => (o.Position - Agent.Position).LengthSquared);

            foreach (var element in allElementsSorted)
            {
                if (MathUtil.CheckForCollisionBetweenCircleAndLine(element.Position, element.Radius, EyeStart, EyeEnd))
                {
                    if (element is FoodElement)
                    {
                        result = Color.Green;
                    }
                    else if (element is Agent)
                    {
                        result = Color.Red;
                    }
                    break;
                }
            }

            // Check if we are hitting the world border
            var x1 = EyeStart.X;
            var y1 = EyeStart.Y;
            var x2 = EyeEnd.X;
            var y2 = EyeEnd.Y;

            if (result == Color.None && (
                x1 <= 0 || World.Width <= x1 ||
                x2 <= 0 || World.Width <= x2 ||
                y1 <= 0 || World.Height <= y1 ||
                y2 <= 0 || World.Height <= y2))
            {
                result = Color.Red;
            }

            return result;
        }
    }

    public enum Color
    {
        None,
        Green,
        Red,
    }
}
