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
        public double Bias { get; set; }
        public IList<double> Weights{ get; set; }

        public double Pulse(IEnumerable<double> inputs)
        {
            var inputsArray = inputs.ToArray();

            var sum = Bias;
            for (int i = 0; i < Weights.Count; i++)
            {
                sum += Weights[i] * inputsArray[i];
            }

            var result = Sigmoid(sum);

            return result;
        }

        private double Sigmoid(double x)
        {
            return 2 / (1 + Math.Exp(-2 * x)) - 1;
        }
    }
}
