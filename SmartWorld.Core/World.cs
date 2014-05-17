using SmartWorld.Core.Config;
using SmartWorld.Core.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    public class World : IPopulation
    {
        public World()
        {
            var config = ConfigManager.Current;
            Height = config.WorldHeight;
            Width = config.WorldWidth;
            Agents = new List<Agent>();

            for (int i = 0; i < config.NumberOfAgents; i++)
            {
                var agentToAdd = Agent.CreateRandomAgend(this);
                Agents.Add(agentToAdd);
            }
        }


        public ICollection<Agent> Agents { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }

        public void Tick()
        {
            // All agents should make a move
            // Remove dead agents and for each of them create a child

        }


        ICollection<IIndividual> IPopulation.Individuals
        {
            get { throw new NotImplementedException(); }
        }

        void IPopulation.CreateIndividual(double[] genotype)
        {
            throw new NotImplementedException();
        }
    }
}
