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
            /// Initiate properties from config
            var config = ConfigManager.Current;
            Height = config.WorldHeight;
            Width = config.WorldWidth;


            // Create random agents
            Agents = new List<Agent>(config.NumberOfAgents);
            for (int i = 0; i < config.NumberOfAgents; i++)
            {
                var agentToAdd = Agent.CreateRandomAgent(this);
                Agents.Add(agentToAdd);
            }

            // EvolutionManager is used to create offspring
            EvolutionManager = new EvolutionManager(this);

            // Create food
            FoodElements = new List<FoodElement>();
            CreateRandomFoodElements(config.NumberOfFoodElements);
        }


        public IList<Agent> Agents { get; private set; }
        public IList<FoodElement> FoodElements { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }

        private EvolutionManager EvolutionManager { get; set; }

        public void Tick()
        {
            // All agents should make a move
            foreach (var agent in Agents)
            {
                agent.Tick();
            }

            // Remove dead agents 
            var numberOfDeaths = 0;
            for (int i = Agents.Count - 1; i >= 0; i--)
            {
                if (Agents[i].IsDead)
                {
                    Agents.RemoveAt(i);
                    numberOfDeaths++;
                }
            }

            // For each of them create a child
            for (int i = 0; i < numberOfDeaths; i++)
            {
                EvolutionManager.CreateChild();
            }
        }

        internal void CreateRandomFoodElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var foodToAdd = FoodElement.CreateRandomFoodElement(this);
                FoodElements.Add(foodToAdd);
            }
        }

        IEnumerable<IIndividual> IPopulation.Individuals
        {
            get
            {
                return Agents.OfType<IIndividual>();
            }
        }

        void IPopulation.CreateIndividual(double[] genotype)
        {
            var agentToAdd = Agent.CreateAgent(this, genotype);
            Agents.Add(agentToAdd);
        }
    }
}
