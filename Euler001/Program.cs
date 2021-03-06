﻿using System;
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

        public static long FunctionChain(long limit)
        {
            return ClosedRange(1, limit - 1).Where(n => n % 3 == 0 || n % 5 == 0).Sum();
        }

        public static long ForLoop(long limit)
        {
            long sum = 0;
            for (long num = 1; num < limit; ++num)
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
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler1.Program), 1);

                yield return factory(nameof(FunctionChain), 1000L, 233168L).Canonical();
                yield return factory(nameof(ForLoop), 1000L, 233168L);

                yield return factory(nameof(FunctionChain), 10L, 23L).Mini();
                yield return factory(nameof(ForLoop), 10L, 23L).Mini();
            }
        }
    }
}
