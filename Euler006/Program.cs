using Euler;
using System;
using System.Collections.Generic;
using System.Linq;
using static Euler.Extension;
using static Euler.Sequence;

namespace Euler6
{
    public class Program
    {
        static void Main(string[] args)
        {
            ManuallySumRanges(100).ConsoleWriteLine();
        }

        public static long ManuallySumRanges(long upTo)
        {
            Func<long, long> square = x => x * x;
            return square(ClosedRange(1, upTo).Sum()) - ClosedRange(1, upTo).Select(x => square(x)).Sum();
        }

       // TODO: implement direct calculation

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler6.Program), 6);

                yield return factory(nameof(ManuallySumRanges), 100, 25164150L).Canonical();
                yield return factory(nameof(ManuallySumRanges), 10, 2640L).Mini();
            }
        }
    }
}
