using System;
using System.Linq;

using static Euler.Mathematical;
using static Euler.Extension;
using System.Collections.Generic;
using Euler;

namespace Euler3
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Factorize(600851475143).Max().ConsoleWriteLine();
        }

        public static long FunctionChain(long num)
        {
            return Factorize(num).Max();
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler3.Program), 3);

                yield return factory(nameof(FunctionChain), 600851475143L, 6857L).Canonical();
                yield return factory(nameof(FunctionChain), 13195L, 29L).Mini();
            }
        }
    }
}
