using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using System.Collections.Generic;
using Euler;

namespace Euler39
{
    public class Program
    {
        static void Main(string[] args)
        {
            CrossSelect(                                                                            
                ClosedRange(1,1000),
                ClosedRange(1,1000),
                (l,r) => (a: l, b: r, c: Math.Sqrt(l.Squared() + r.Squared()))
            )
            .Where(pt => pt.a + pt.b + pt.c < 1000)
            .Where(pt => pt.c % 1 == 0)
            .Select(pt => pt.a + pt.b + (long)(pt.c))
            .GroupBy(p => p)
            .OrderByDescending(g => g.Count())
            .First()
            .Key
            .ConsoleWriteLine();
        }

        public static long SimpleCrossSelect(long max)
        {
            return CrossSelect(
                ClosedRange(1, max),
                ClosedRange(1, max),
                (l, r) => (a: l, b: r, c: Math.Sqrt(l.Squared() + r.Squared()))
            )
            .Where(pt => pt.a + pt.b + pt.c < max)
            .Where(pt => pt.c % 1 == 0)
            .Select(pt => pt.a + pt.b + (long)(pt.c))
            .GroupBy(p => p)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler39.Program), 39);

                yield return factory(nameof(SimpleCrossSelect), 1000L, 840L).Canonical();
            }
        }
    }
}
