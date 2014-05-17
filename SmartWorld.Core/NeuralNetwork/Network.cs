using SmartWorld.Core.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.NeuralNetwork
{
    public class Network
    {
        public Layer HiddenLayer { get; private set; }
        public Layer OutputLayer { get; private set; }

        public void SetRandomWeights()
        {
            // TODO: Set random weights and biases
        }
    }
}
