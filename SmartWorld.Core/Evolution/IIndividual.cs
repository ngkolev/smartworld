using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.Evolution
{
    public interface IIndividual
    {
        double[] Genotype { get; }
        double Fitness { get; }
    }
}
