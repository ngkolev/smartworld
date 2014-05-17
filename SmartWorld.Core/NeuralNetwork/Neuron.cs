using SmartWorld.Core.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.NeuralNetwork
{
    public class Neuron
    {
        public double Bias { get; private set; }
        public ICollection<double> Weights{ get; private set; }

        public double Pulse(ICollection<double> inputs)
        {
            throw new NotImplementedException();
        }
    }
}
