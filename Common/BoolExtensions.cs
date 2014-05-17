using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class BoolExtensions
    {
        public static double AsDouble(this bool b)
        {
            return b ? 1.0 : 0.0;
        }
    }
}
