using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using System.Collections.Generic;
using Euler;

namespace Euler10
{
    public class Program
    {
        static void Main(string[] args)
        {
            Primes().TakeWhile(p => p < 2000000).Sum().ConsoleWriteLine();
        }

        public static long FunctionChain(long bound)
        {
            return Primes().TakeWhile(p => p < bound).Sum();
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler10.Program), 10);

                yield return factory(nameof(FunctionChain), 2000000L, 142913828922L).Canonical();
                yield return factory(nameof(FunctionChain), 10L, 17L).Mini();
            }
        }
    }
}
