using Euler;
using System;
using System.Collections.Generic;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;

namespace Euler7
{
    public class Program
    {
        static void Main(string[] args)
        {
            Primes().Skip(10000).First().ConsoleWriteLine();
        }

        public static long SkipFirst(int n)
        {
            return Primes().Skip(n - 1).First();
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<int>(typeof(Euler7.Program), 7);

                yield return factory(nameof(SkipFirst), 10001, 104743L).Canonical();
                yield return factory(nameof(SkipFirst), 6, 13L).Mini();
            }
        }
    }
}
