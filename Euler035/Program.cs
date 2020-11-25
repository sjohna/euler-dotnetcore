using System;
using System.Linq;
using System.Collections.Generic;

using static Euler.Sequence;
using static Euler.Extension;
using static Euler.Mathematical;
using Euler;

namespace Euler35
{
    public class Program
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

        public static long CachePrimesAsList(long upTo)
        {
            var primes = Primes().TakeWhile(p => p < upTo).ToList();

            return primes.Where(p => CircularShifts(p).All(n => primes.Contains(n))).Count();
        }

        public static long CachePrimesAsHashSet(long upTo)
        {
            var primes = Primes().TakeWhile(p => p < upTo).ToHashSet();

            return primes.Where(p => CircularShifts(p).All(n => primes.Contains(n))).Count();
        }

        public static long CallIsPrimeEachTime(long upTo)
        {
            return Primes().TakeWhile(p => p < upTo).Where(p => CircularShifts(p).All(n => IsPrime(n))).Count();
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler35.Program), 35);

                yield return factory(nameof(CachePrimesAsHashSet), 1000000L, 55L).Canonical();
                yield return factory(nameof(CachePrimesAsHashSet), 100L, 13L).Mini();

                yield return factory(nameof(CallIsPrimeEachTime), 1000000L, 55L);
                yield return factory(nameof(CallIsPrimeEachTime), 100L, 13L).Mini();

                yield return factory(nameof(CachePrimesAsList), 1000000L, 55L).Slow();
                yield return factory(nameof(CachePrimesAsList), 100L, 13L).Mini();
            }
        }
    }
}
