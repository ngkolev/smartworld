using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringExtensions
    {
        public static double AsDouble(this string s)
        {
            return double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        public static int AsInt(this string s)
        {
            return int.Parse(s);
        }

        public static string Formatted(this string s, params object[] args)
        {
            return String.Format(s, args);
        }
    }
}
