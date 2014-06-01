using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.NeuralNetwork
{
    public class Layer
    {
        public Layer(int numberOfInputs, int numberOfNeurons)
        {
            Neurons = new List<Neuron>(numberOfNeurons);
            for (int i = 0; i < numberOfNeurons; i++)
            {
                var neuronToAdd = new Neuron(numberOfInputs);
                Neurons.Add(neuronToAdd);
            }
        }

        public IList<Neuron> Neurons { get; private set; }

        public override string ToString()
        {
            var resultBuffer = new StringBuilder();
            for (int i = 0; i < Neurons.Count; i++)
            {
                resultBuffer.AppendFormat("\tN{0}:{1}", i, Neurons[i]);
            }

            return resultBuffer.ToString();
        }
    }
}
