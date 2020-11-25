using Euler;
using System;
using System.Collections.Generic;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;

namespace Euler4
{
    public static class Program
    {
        static void Main(string[] args)
        {
            FunctionChain(3).ConsoleWriteLine();
        }

        public static long FunctionChain(int numDigits)
        {
            long min = 1, max = 10;

            for (int i = 1; i < numDigits; ++i)
            {
                min = max;
                max *= 10;
            }

            max -= 1;

            return CrossSelect(ClosedRange(min, max), ClosedRange(min, max), (x, y) => x * y)
                .Where(n => n.IsPalindromeNumber())
                .Max();
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<int>(typeof(Euler4.Program), 4);

                yield return factory(nameof(FunctionChain), 3, 906609L).Canonical();
                yield return factory(nameof(FunctionChain), 2, 9009L).Mini();
            }
        }
    }
}
