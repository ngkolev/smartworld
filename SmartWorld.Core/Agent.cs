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
            Health = config.AgentInitialHealth;
            Radius = config.AgentRadius;
            HealthFactor = config.AgentHealthFactor;
            AgeFactor = config.AgentAgeFactor;
        }

        public Vector Position { get; private set; }
        public Vector LookAt { get; private set; }
        public bool IsDead { get; private set; }
        public int Age { get; private set; }
        public int Health { get; private set; }

        private Network Brain { get; set; }
        private World World { get; set; }
        private double Speed { get; set; }
        private double Radius { get; set; }
        private double HealthFactor { get; set; }
        private double AgeFactor { get; set; }

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
                return Age * AgeFactor + Health * HealthFactor;
            }
        }


        public void Tick()
        {
            // Tell the brain what is happening
            // Get the response from brain and make move
            // Check if we have got food. If we have then tell the world to create some more food
            throw new NotImplementedException();


            CheckForCollisionWithBorder();

            if (!IsDead)
            {
                CheckForCollisionWithOtherAgent();
            }

            if (!IsDead)
            {
                ReduceHealth();
                CheckForStarvation();
            }
        }

        private void CheckForCollisionWithBorder()
        {
            if (Position.X < 0 || World.Width < Position.X ||
                Position.Y < 0 || World.Height < Position.Y)
            {
                IsDead = true;
            }
        }

        private void CheckForCollisionWithOtherAgent()
        {
            foreach (var agent in World.Agents)
            {
                if (agent != this && MathUtil.CheckForCollision(Position, Radius, agent.Position, agent.Radius))
                {
                    IsDead = true;
                    break;
                }
            }
        }

        private void ReduceHealth()
        {
            Health--;
        }

        private void CheckForStarvation()
        {
            if (Health < 0)
            {
                IsDead = true;
            }
        }


        public static Agent CreateRandomAgent(World world)
        {
            throw new NotImplementedException();
        }

        public static Agent CreateAgent(double[] genotype)
        {
            // NOTE: Don't forget it to place it (reuse CreateRandomAgent method)
            throw new NotImplementedException();
        }
    }
}
