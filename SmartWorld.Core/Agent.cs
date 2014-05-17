using Common;
using SmartWorld.Core.Config;
using SmartWorld.Core.Evolution;
using SmartWorld.Core.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    public class Agent : IIndividual
    {
        private Agent(World world, Vector position, Vector lookAt)
        {
            var config = ConfigManager.Current;

            World = world;
            Position = position;
            LookAt = lookAt;
            Speed = config.AgentSpeed;
            Health = config.InitialAgentHealth;
        }

        public Vector Position { get; private set; }
        public Vector LookAt { get; private set; }
        public bool IsDead { get; private set; }
        public int Age { get; private set; }
        public int Health { get; private set; }


        private Network Brain { get; set; }
        private World World { get; set; }
        private double Speed { get; set; }

        public double[] Genotype
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public double Fitness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public static Agent CreateRandomAgend(World world)
        {
            throw new NotImplementedException();
        }
    }
}
