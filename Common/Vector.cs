using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Vector
    {
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public Vector Normalized
        {
            get
            {
                var distance = Math.Sqrt(X * X + Y * Y);
                return new Vector(X / distance, Y / distance);
            }
        }


        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator *(Vector left, Vector right)
        {
            return new Vector(left.X * right.X, left.Y * right.Y);
        }

        public static Vector operator *(Vector left, double right)
        {
            return new Vector(left.X * right, left.Y * right);
        }

        public static Vector operator *(double left, Vector right)
        {
            return new Vector(left * right.X, left * right.Y);
        }
    }
}
