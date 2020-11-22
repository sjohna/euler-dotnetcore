using System;
using System.Linq;
using System.Collections.Generic;

using static Euler.Extension;
using static Euler.Sequence;
using Euler;

namespace Euler1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FunctionChain(999).ConsoleWriteLine();
        }

        public static long FunctionChain(long max)
        {
            return ClosedRange(1, max).Where(n => n % 3 == 0 || n % 5 == 0).Sum();
        }

        public static long ForLoop(long max)
        {
            long sum = 0;
            for (long num = 1; num <= max; ++num)
            {
                if (num % 3 == 0 || num % 5 == 0)
                {
                    sum += num;
                }
            }

            return sum;
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                yield return new EulerProblemInstance<long>(
                    ProblemNumber: 1,
                    Method: nameof(FunctionChain),
                    ParameterRepresentation: "999",
                    ExpectedResult: 233168L,
                    Execute: () => FunctionChain(999L),
                    IsCanonical: true);

                yield return new EulerProblemInstance<long>(
                    ProblemNumber: 1,
                    Method: nameof(ForLoop),
                    ParameterRepresentation: "999",
                    ExpectedResult: 233168L,
                    Execute: () => ForLoop(999L));
            }
        }
    }
}
