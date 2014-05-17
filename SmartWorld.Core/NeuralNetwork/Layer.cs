using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.NeuralNetwork
{
    public class Layer
    {
        public IList<Neuron> Neurons { get; private set; }
    }
}
