using Common;
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

        public Network(int inputsCount, int hiddenLayerNeuronCount, int outputLayerNeuronCount)
        {
            HiddenLayer = new Layer(inputsCount, hiddenLayerNeuronCount);
            OutputLayer = new Layer(hiddenLayerNeuronCount, outputLayerNeuronCount);
        }

        public IEnumerable<double> Pulse(IEnumerable<double> inputs)
        {
            var hiddenLayerOutputs = HiddenLayer.Neurons.Select(n => n.Pulse(inputs));
            var outputLayerOutputs = OutputLayer.Neurons.Select(n => n.Pulse(hiddenLayerOutputs));

            return outputLayerOutputs;
        }

        public void SetRandomWeights()
        {
            SetRandomNeuronWeights(HiddenLayer);
            SetRandomNeuronWeights(OutputLayer);
        }

        private static void SetRandomNeuronWeights(Layer layer)
        {
            var random = RandomHolder.Random;
            foreach (var neuron in layer.Neurons)
            {
                for (int i = 0; i < neuron.Weights.Count; i++)
                {
                    neuron.Weights[i] = random.NextDouble(0.1, 0.9);
                }

                neuron.Bias = random.NextDouble(-0.9, 0.9);
            }
        }
    }
}
