using Common;
using SmartWorld.Core.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    public class Agent : IIndividual
    {
        private Agent(World world, Vector position, Vector lookAt, double speed)
        {
            World = world;
            Position = position;
            LookAt = lookAt;
            Speed = speed;
        }

        public World World { get; private set; }
        public Vector Position { get; private set; }
        public Vector LookAt { get; private set; }
        public bool IsDead { get; private set; }
        public double Speed { get; private set; }
        public int Age { get; private set; }
        public int CollectedFood { get; private set; }

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

        public static Agent CreateRandomAgend(World world, double speed)
        {
            throw new NotImplementedException();
        }
    }
}
