using Euler;
using System.Collections.Generic;
using System.Linq;

using static Euler.Extension;
using static Euler.Mathematical;
using static Euler.Sequence;

namespace Euler5
{
    public static class Program
    {
        static void Main(string[] args)
        {
            IterativeLCM(20).ConsoleWriteLine();
        }

        public static long IterativeLCM(long max)
        {
            return ClosedRange(1, max).Aggregate(LCM);
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler5.Program), 5);

                yield return factory(nameof(IterativeLCM), 20, 232792560L).Canonical();
                yield return factory(nameof(IterativeLCM), 10, 2520L).Mini();
            }
        }
    }
}
