using Common;
using SmartWorld.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.Evolution
{
    internal class EvolutionManager
    {
        public EvolutionManager(IPopulation population)
        {
            Population = population;
            MutationRate = ConfigManager.Current.MutationRate;
        }

        public IPopulation Population { get; private set; }

        private double MutationRate { get; set; }

        public void CreateChild()
        {
            // Get parents
            var bestTwoAgents = Population.Individuals
                .OrderByDescending(i => i.Fitness)
                .Take(5)
                .ToArray();

            if (bestTwoAgents.Length >= 2)
            {
                var mother = bestTwoAgents[RandomHolder.Random.Next(5)].Genotype;
                var father = bestTwoAgents[RandomHolder.Random.Next(5)].Genotype;

                // Crossover
                var genotypeLength = mother.Length;
                var splitLine = RandomHolder.Random.Next(genotypeLength);
                var result = new double[genotypeLength];
                for (int i = 0; i < splitLine; i++)
                {
                    result[i] = mother[i];
                }

                for (int i = splitLine; i < genotypeLength; i++)
                {
                    result[i] = father[i];
                }

                // Mutation
                var random = RandomHolder.Random;
                var neuronToChangeCount = (int)MutationRate * genotypeLength;
                for (int i = 0; i < neuronToChangeCount; i++)
                {
                    var j = random.Next(genotypeLength);
                    result[j] += RandomHolder.Random.NextDouble(-0.3, 0.3);
                }

                Population.CreateIndividual(result);
            }
        }
    }
}
