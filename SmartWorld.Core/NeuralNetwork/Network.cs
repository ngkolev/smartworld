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

        public IEnumerable<double> Pulse(IEnumerable<double> inputs)
        {
            var hiddenLayerOutputs = HiddenLayer.Neurons.Select(n => n.Pulse(inputs));
            var outputLayerOutputs = OutputLayer.Neurons.Select(n => n.Pulse(hiddenLayerOutputs));

            return outputLayerOutputs;
        }

        public void SetRandomWeights()
        {
            // TODO: Set random weights and biases
            throw new NotImplementedException();
        }
    }
}
