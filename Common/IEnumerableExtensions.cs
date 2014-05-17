using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<List<T>> InSetsOf<T>(this IEnumerable<T> source, int max)
        {
            List<T> toReturn = new List<T>(max);
            foreach (var item in source)
            {
                toReturn.Add(item);
                if (toReturn.Count == max)
                {
                    yield return toReturn;
                    toReturn = new List<T>(max);
                }
            }
            if (toReturn.Any())
            {
                yield return toReturn;
            }
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> items)
        {
            return items.Take(items.Count() - 1);
        }

        public static T TakeLast<T>(this IEnumerable<T> items)
        {
            return items.ElementAt(items.Count() - 1);
        }
    }
}
