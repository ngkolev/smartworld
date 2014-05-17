using SmartWorld.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.Evolution
{
    public class EvolutionManager
    {
        public EvolutionManager(IPopulation population)
        {
            Population = population;
            MutationRate = ConfigManager.MutationRate;
            Random = new Random();
        }

        public IPopulation Population { get; private set; }

        private double MutationRate { get; set; }
        private Random Random { get; set; }

        public void CreateChild()
        {
            // Get parents
            var bestTwoAgents = Population.Individuals
                .OrderByDescending(i => i.Fitness)
                .Take(2)
                .ToArray();

            var mother = bestTwoAgents[0].Genotype;
            var father = bestTwoAgents[1].Genotype;

            // Crossover
            var genotypeLength = mother.Length;
            var splitLine = genotypeLength / 2;
            var result = new List<double>(genotypeLength);
            for (int i = 0; i < splitLine; i++)
            {
                result[i] = mother[i];
            }

            for (int i = splitLine; i < genotypeLength; i++)
            {
                result[i] = father[i];
            }

            // Mutation
            if (Random.NextDouble() < MutationRate)
            {
                var i = Random.Next(genotypeLength);
                var j = Random.Next(genotypeLength);

                var temporal = result[i];
                result[i] = result[j];
                result[j] = temporal;
            }
        }
    }
}
