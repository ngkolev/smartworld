using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DoubleExtensions
    {
        public static int Rounded(this double n)
        {
            return (int)Math.Round(n);
        }
    }
}
