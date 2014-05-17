using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class MathUtil
    {
        public static bool CheckForCollision(Vector p1, double r1, Vector p2, double r2)
        {
            return (p2.X - p1.X).Sqr() + (p1.Y - p2.Y).Sqr() <= (r1 + r2).Sqr();
        }

        public static double Sqr(this double num)
        {
            return num * num;
        }
    }
}
