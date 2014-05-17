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

        public double LengthSquared
        {
            get
            {
                return X.Sqr() + Y.Sqr();
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(LengthSquared);
            }
        }

        public Vector Rotated(double angle)
        {
            var x = X * Math.Cos(angle) - Y * Math.Sin(angle);
            var y = X * Math.Sin(angle) + Y * Math.Cos(angle);

            return new Vector(x, y);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator +(Vector left, double right)
        {
            return new Vector(left.X + right, left.Y + right);
        }

        public static Vector operator +(double left, Vector right)
        {
            return new Vector(left + right.X, left + right.Y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
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

        public static double operator ^(Vector left, Vector right)
        {
            return left.X * right.X + left.Y * right.Y;
        }


        public static Vector CreateRandomVector(double minX, double maxX, double minY, double maxY)
        {
            var random = RandomHolder.Random;
            var x = random.NextDouble(minX, maxX);
            var y = random.NextDouble(minY, maxY);

            return new Vector(x, y);
        }

        public static Vector CreateRandomVector(double maxX, double maxY)
        {
            return CreateRandomVector(0, maxX, 0, maxY);
        }
    }
}
