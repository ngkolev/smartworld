using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    public class World
    {
        public World(double height, double width, int numberOfAgents)
        {
            Height = height;
            Width = width;
            Agents = new List<Agent>();

            for (int i = 0; i < numberOfAgents; i++)
            {
                var agentToAdd= Agent.CreateRandomAgend(this, 1); // FIXME: Hardcoded agent speed
                Agents.Add(agentToAdd);
            }
        }

        public ICollection<Agent> Agents { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
