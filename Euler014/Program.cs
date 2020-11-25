using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using System.Collections.Generic;
using Euler;

namespace Euler14
{
    public class Program
    {
        static void Main(string[] args)
        {
            ClosedRange(1,1000000)
            .Select(n => (n, (long)(Collatz(n).Count())))
            .Aggregate((a, b) => (a.Item2 > b.Item2) ? a : b)
            .Item1
            .ConsoleWriteLine();
        }

        public static long FunctionChain(long upperBound)
        {
            return ClosedRange(1, upperBound)
            .Select(n => (n, (long)(Collatz(n).Count())))
            .Aggregate((a, b) => (a.Item2 > b.Item2) ? a : b)
            .Item1;
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler14.Program), 14);

                yield return factory(nameof(FunctionChain), 1000000L, 837799L).Canonical();
            }
        }

    }
}
