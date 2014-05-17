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
        }

        public IPopulation Population { get; private set; }

        public void CreateChild()
        {
            throw new NotImplementedException();
        }
    }
}
