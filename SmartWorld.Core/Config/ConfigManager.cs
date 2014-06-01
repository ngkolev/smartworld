using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SmartWorld.Core.Config
{
    public class ConfigManager
    {
        private static readonly ConfigManager current = new ConfigManager();
        private ConfigManager()
        {
            var appSettings = ConfigurationManager.AppSettings;

            WorldHeight = appSettings["WorldHeight"].AsDouble();
            WorldWidth = appSettings["WorldWidth"].AsDouble();
            NumberOfAgents = appSettings["NumberOfAgents"].AsInt();
            NumberOfFoodElements = appSettings["NumberOfFoodElements"].AsInt();
            MutationRate = appSettings["MutationRate"].AsDouble();
            NumberOfNeuronsInHiddenLayer = appSettings["NumberOfNeuronsInHiddenLayer"].AsInt();
            AgentSpeed = appSettings["AgentSpeed"].AsDouble();
            AgentEyeAngle = appSettings["AgentEyeAngle"].AsDouble();
            AgentEyeDepth = appSettings["AgentEyeDepth"].AsDouble();
            AgentRotationAngle = appSettings["AgentRotationAngle"].AsDouble();
            AgentInitialHealth = appSettings["AgentInitialHealth"].AsInt();
            AgentRadius = appSettings["AgentRadius"].AsDouble();
            AgentHealthFactor = appSettings["AgentHealthFactor"].AsDouble();
            AgentAgeFactor = appSettings["AgentAgeFactor"].AsDouble();
            FoodElementRadius = appSettings["FoodElementRadius"].AsDouble();
            FoodElementHealthPoints = appSettings["FoodElementHealthPoints"].AsInt();
            TickLength = appSettings["TickLength"].AsInt();

            ShouldLogAgentNeuralNetworks = true;
        }

        public static ConfigManager Current
        {
            get { return current; }
        }

        public double WorldHeight { get; private set; }
        public double WorldWidth { get; private set; }
        public int NumberOfAgents { get; private set; }
        public int NumberOfFoodElements { get; private set; }
        public double MutationRate { get; private set; }
        public int NumberOfNeuronsInHiddenLayer { get; private set; }

        public double AgentSpeed { get; private set; }
        public double AgentEyeAngle { get; private set; }
        public double AgentEyeDepth { get; private set; }
        public int AgentInitialHealth { get; private set; }
        public double AgentRotationAngle { get; private set; }
        public double AgentRadius { get; private set; }
        public double AgentHealthFactor { get; private set; }
        public double AgentAgeFactor { get; private set; }

        public double FoodElementRadius { get; private set; }
        public int FoodElementHealthPoints { get; private set; }

        public int TickLength { get; private  set; }

        public bool ShouldLogAgentNeuralNetworks { get; set; }
    }
}
