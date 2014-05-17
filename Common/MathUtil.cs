using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class MathUtil
    {
        public static bool CheckForCollisionBetweenCircles(Vector p1, double r1, Vector p2, double r2)
        {
            return (p2.X - p1.X).Sqr() + (p1.Y - p2.Y).Sqr() <= (r1 + r2).Sqr();
        }

        public static bool CheckForCollisionBetweenCircleAndLine(Vector p, double r, Vector s, Vector e)
        {
            var result = false;
            var d = e - s;
            var f = s - p;
            var a = d ^ d;
            var b = 2 * f ^ d;
            var c = f ^ f + (-r * r);

            var discriminant = b * b - 4 * a * c;
            if (discriminant >= 0)
            {
                discriminant = Math.Sqrt(discriminant);
                var t1 = (-b - discriminant) / (2 * a);
                var t2 = (-b + discriminant) / (2 * a);

                if (t1 >= 0 && t1 <= 1)
                {
                    result = true;
                }

                if (t2 >= 0 && t2 <= 1)
                {
                    result = true;
                }
            }

            return result;
        }

        public static double Sqr(this double num)
        {
            return num * num;
        }
    }
}
