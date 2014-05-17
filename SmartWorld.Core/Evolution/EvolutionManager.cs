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
            var random = RandomHolder.Random;
            if (random.NextDouble() < MutationRate)
            {
                var i = random.Next(genotypeLength);
                var j = random.Next(genotypeLength);

                var temporal = result[i];
                result[i] = result[j];
                result[j] = temporal;
            }
        }
    }
}
