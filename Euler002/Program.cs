using System;
using System.Linq;
using static Euler.Sequence;
using static Euler.Extension;
using Euler;
using System.Collections.Generic;

namespace Euler2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Fibonacci().TakeWhile(n => n < 4000000L).Where(n => n % 2 == 0).Sum().ConsoleWriteLine();
        }

        public static long FunctionChain(long limit)
        {
            return Fibonacci().TakeWhile(n => n < limit).Where(n => n % 2 == 0).Sum();
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler2.Program), 2);

                yield return factory(nameof(FunctionChain), 4000000L, 4613732L).Canonical();
            }
        }
    }
}
