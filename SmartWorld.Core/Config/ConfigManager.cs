using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core.Config
{
    public class ConfigManager
    {
        private static readonly ConfigManager current = new ConfigManager();
        private ConfigManager()
        {
            // TODO: Load configuration
        }

        public static ConfigManager Current
        {
            get { return current; }
        }

        public double WorldHeight { get; private set; }
        public double WorldWidth { get; private set; }
        public int NumberOfAgents { get; private set; }
        public double AgentSpeed { get; private set; }
        public int InitialAgentHealth { get; private set; }

        public double AgentRadius { get; set; }

        public static double MutationRate { get; set; }
    }
}
