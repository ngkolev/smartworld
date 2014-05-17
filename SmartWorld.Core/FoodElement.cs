using Common;
using SmartWorld.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWorld.Core
{
    public class FoodElement : IElement
    {
        private FoodElement(Vector position)
        {
            var config = ConfigManager.Current;

            Position = position;
            Radius = config.FoodElementRadius;
            HealthPoints = config.FoodElementHealthPoints;
        }

        public Vector Position { get; private set; }
        public double Radius { get; private set; }
        public int HealthPoints { get; private set; }

        public static FoodElement CreateRandomFoodElement(World world)
        {
            var maxX = (world.Width - ConfigManager.Current.FoodElementRadius);
            var maxY = (world.Height - ConfigManager.Current.FoodElementRadius);
            var position = Vector.CreateRandomVector(maxX, maxY);

            return new FoodElement(position);
        }
    }
}
