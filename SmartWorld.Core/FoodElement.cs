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
            var min = ConfigManager.Current.FoodElementRadius;
            var maxX = (world.Width - min * 2);
            var maxY = (world.Height - min * 2);
            var position = Vector.CreateRandomVector(min, maxX, min, maxY);

            return new FoodElement(position);
        }
    }
}
