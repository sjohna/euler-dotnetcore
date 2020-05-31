using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;

namespace Euler39
{
    class Program
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
    }
}
