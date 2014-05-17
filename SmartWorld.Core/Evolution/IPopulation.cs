using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.Evolution
{
    public interface IPopulation
    {
        IEnumerable<IIndividual> Individuals { get; }
        void CreateIndividual(double[] genotype);
    }
}
