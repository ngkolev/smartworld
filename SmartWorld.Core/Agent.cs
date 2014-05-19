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
    public class Agent : IIndividual, IElement
    {
        // Inputs
        private const int NUMBER_OF_INPUTS = 11;
        private const int INDEX_LEFT_RED_EYE = 0;
        private const int INDEX_RIGHT_RED_EYE = 1;
        private const int INDEX_LEFT_GREEN_EYE = 2;
        private const int INDEX_RIGHT_GREEN_EYE = 3;
        private const int INDEX_CENTER_RED_EYE = 4;
        private const int INDEX_CENTER_GREEN_EYE = 5;
        private const int INDEX_OUTER_LEFT_RED_EYE = 6;
        private const int INDEX_OUTER_RIGHT_RED_EYE = 7;
        private const int INDEX_OUTER_LEFT_GREEN_EYE = 8;
        private const int INDEX_OUTER_RIGHT_GREEN_EYE = 9;
        private const int INDEX_HEALTH = 10;

        // Outputs
        private const int NUMBER_OF_OUTPUTS = 2;
        private const int INDEX_TURN_LEFT = 0;
        private const int INDEX_TURN_RIGHT = 1;


        private Agent(World world, Vector position, Vector lookAt)
        {
            var config = ConfigManager.Current;

            World = world;
            Position = position;
            LookAt = lookAt;
            Speed = config.AgentSpeed;
            EyeAngle = config.AgentEyeAngle;
            Health = config.AgentInitialHealth;
            Radius = config.AgentRadius;
            RotationAngle = config.AgentRotationAngle;
            HealthFactor = config.AgentHealthFactor;
            AgeFactor = config.AgentAgeFactor;

            Brain = new Network(NUMBER_OF_INPUTS, config.NumberOfNeuronsInHiddenLayer, NUMBER_OF_OUTPUTS);
        }

        public Vector Position { get; private set; }
        public Vector LookAt { get; private set; }
        public bool IsDead { get; private set; }
        public int Age { get; private set; }
        public int Health { get; private set; }
        public double Radius { get; private set; }

        private World World { get; set; }
        private Network Brain { get; set; }
        private double Speed { get; set; }
        private double EyeAngle { get; set; }
        private double RotationAngle { get; set; }
        private double HealthFactor { get; set; }
        private double AgeFactor { get; set; }

        public double[] Genotype
        {
            get
            {
                var hiddenLayerGenotype = GetLayerGenotype(Brain.HiddenLayer);
                var outputLayerGenotype = GetLayerGenotype(Brain.OutputLayer);
                var genotype = hiddenLayerGenotype.Concat(outputLayerGenotype);

                return genotype.ToArray();
            }
            private set
            {
                var hiddenLayerNeuronCount = Brain.HiddenLayer.Neurons.Count;
                var hiddenLayerGenotypeSize = hiddenLayerNeuronCount * (NUMBER_OF_INPUTS + 1);
                var hiddenLayerGenotype = value.Take(hiddenLayerGenotypeSize);
                var outputLayerGenotype = value.Skip(hiddenLayerGenotypeSize);

                var hiddenLayerNeuronsGenotype = hiddenLayerGenotype.InSetsOf(NUMBER_OF_INPUTS + 1).ToList();
                var outputLayerNeuronsGenotype = outputLayerGenotype.InSetsOf(hiddenLayerNeuronCount + 1).ToList();

                for (int i = 0; i < Brain.HiddenLayer.Neurons.Count; i++)
                {
                    var weights = hiddenLayerNeuronsGenotype[i].SkipLast();
                    var bias = hiddenLayerNeuronsGenotype[i].TakeLast();
                    Brain.HiddenLayer.Neurons[i].Weights = weights.ToList();
                    Brain.HiddenLayer.Neurons[i].Bias = bias;
                }

                for (int i = 0; i < Brain.OutputLayer.Neurons.Count; i++)
                {
                    var weights = outputLayerNeuronsGenotype[i].SkipLast();
                    var bias = outputLayerNeuronsGenotype[i].TakeLast();
                    Brain.OutputLayer.Neurons[i].Weights = weights.ToList();
                    Brain.OutputLayer.Neurons[i].Bias = bias;
                }
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
            MakeMove();
            CheckForCollisionWithFood();

            CheckForCollisionWithBorder();

            if (!IsDead)
            {
                CheckForCollisionWithOtherAgent();
            }

            if (!IsDead)
            {
                MakeOlder();
                CheckForStarvation();
            }
        }

        private void MakeMove()
        {
            // Left eye
            var leftEye = new EyeManager(World, this, -EyeAngle);
            var leftColor = leftEye.See();
            var leftColorRed = leftColor == Color.Red;
            var leftColorGreen = leftColor == Color.Green;

            // Left outer eye
            var leftOuterEye = new EyeManager(World, this, -2 * EyeAngle);
            var leftOuterColor = leftOuterEye.See();
            var leftOuterColorRed = leftOuterColor == Color.Red;
            var leftOuterColorGreen = leftOuterColor == Color.Green;

            // Right eye
            var rightEye = new EyeManager(World, this, EyeAngle);
            var rightColor = rightEye.See();
            var rightColorRed = rightColor == Color.Red;
            var rightColorGreen = rightColor == Color.Green;

            // Right outer eye
            var rightOuterEye = new EyeManager(World, this, 2 * EyeAngle);
            var rightOuterColor = rightOuterEye.See();
            var rightOuterColorRed = rightOuterColor == Color.Red;
            var rightOuterColorGreen = rightOuterColor == Color.Green;

            // Center eye
            var centerEye = new EyeManager(World, this, 0);
            var centerColor = centerEye.See();
            var centerColorRed = centerColor == Color.Red;
            var centerColorGreen = centerColor == Color.Green;

            // Inputs
            var inputs = new double[NUMBER_OF_INPUTS];

            inputs[INDEX_LEFT_RED_EYE] = leftColorRed.AsDouble();
            inputs[INDEX_LEFT_GREEN_EYE] = leftColorGreen.AsDouble();
            inputs[INDEX_RIGHT_RED_EYE] = rightColorRed.AsDouble();
            inputs[INDEX_RIGHT_GREEN_EYE] = rightColorGreen.AsDouble();
            inputs[INDEX_OUTER_LEFT_RED_EYE] = leftOuterColorRed.AsDouble();
            inputs[INDEX_OUTER_LEFT_GREEN_EYE] = leftOuterColorGreen.AsDouble();
            inputs[INDEX_OUTER_RIGHT_RED_EYE] = rightOuterColorRed.AsDouble();
            inputs[INDEX_OUTER_RIGHT_GREEN_EYE] = rightOuterColorGreen.AsDouble();
            inputs[INDEX_CENTER_RED_EYE] = centerColorRed.AsDouble();
            inputs[INDEX_CENTER_GREEN_EYE] = centerColorGreen.AsDouble();
            inputs[INDEX_HEALTH] = Health / 1000.0;

            // Pulse
            var outputs = Brain.Pulse(inputs).ToArray();
            var turnLeft = outputs[INDEX_TURN_LEFT] > 0;
            var turnRight = outputs[INDEX_TURN_RIGHT] > 0;

            // Move
            if (turnLeft)
            {
                LookAt = LookAt.Rotated(-RotationAngle);
            }

            if (turnRight)
            {
                LookAt = LookAt.Rotated(RotationAngle);
            }

            Position = Position + LookAt * Speed;
        }

        private void CheckForCollisionWithFood()
        {
            // Check if we have food to eat
            var eatenFood = new List<FoodElement>();
            foreach (var food in World.FoodElements)
            {
                if (MathUtil.CheckForCollisionBetweenCircles(Position, Radius, food.Position, food.Radius))
                {
                    Health += food.HealthPoints;
                    eatenFood.Add(food);
                }
            }
            // Remove eaten food
            foreach (var foodToRemove in eatenFood)
            {
                World.FoodElements.Remove(foodToRemove);
            }

            // Add new food
            World.CreateRandomFoodElements(eatenFood.Count);
        }

        private void CheckForCollisionWithBorder()
        {
            if (Position.X < Radius || World.Width < Position.X + Radius ||
                Position.Y < Radius || World.Height < Position.Y + Radius)
            {
                IsDead = true;
            }
        }

        private void CheckForCollisionWithOtherAgent()
        {
            foreach (var agent in World.Agents)
            {
                if (agent != this && MathUtil.CheckForCollisionBetweenCircles(Position, Radius, agent.Position, agent.Radius))
                {
                    agent.IsDead = true;
                    IsDead = true;
                    break;
                }
            }
        }

        private void MakeOlder()
        {
            Health--;
            Age++;
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
            var agent = CreateAgentWithRandomPosition(world);
            agent.Brain.SetRandomWeights();
            return agent;
        }

        public static Agent CreateAgent(World world, double[] genotype)
        {
            var agent = CreateAgentWithRandomPosition(world);
            agent.Genotype = genotype;
            return agent;
        }

        private static Agent CreateAgentWithRandomPosition(World world)
        {
            // Randomize position
            var min = ConfigManager.Current.AgentRadius;
            var maxX = (world.Width - min * 2);
            var maxY = (world.Height - min * 2);
            var position = Vector.CreateRandomVector(min, maxX, min, maxY);

            // Randomize look direction
            var lookAtUnnormalized = Vector.CreateRandomVector(-1, 1, -1, 1);
            if (lookAtUnnormalized.LengthSquared == 0) // Ensure that we haven't picked the null vector
            {
                lookAtUnnormalized = new Vector(1, 0);
            }

            var lookAt = lookAtUnnormalized.Normalized;

            return new Agent(world, position, lookAt);
        }

        private static IEnumerable<double> GetLayerGenotype(Layer layer)
        {
            return layer.Neurons.SelectMany(n => n.Weights.Concat(new[] { n.Bias }));
        }
    }
}
