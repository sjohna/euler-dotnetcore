using System;
using System.Linq;
using System.Collections.Generic;

using static Euler.Sequence;
using static Euler.Extension;

namespace Euler35
{
    class Program
    {
        public static IEnumerable<long> CircularShifts(long num)
        {
            var numString = num.ToString();

            for (int i = 0; i < numString.Length; ++i)
            {
                yield return Convert.ToInt64(numString.Substring(i) + numString.Substring(0,i));
            }
        }

        static void Main(string[] args)
        {
            var primes = Primes().TakeWhile(p => p < 1000000).ToList();

            primes.Where(p => CircularShifts(p).All(n => primes.Contains(n))).Count().ConsoleWriteLine();
        }
    }
}
