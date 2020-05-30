using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;

namespace Euler9
{
    class Program
    {
        static void Main(string[] args)
        {
            CrossSelect(                                                                            // generate tuples where Item1 + Item2 + Item3 == 1000
                ClosedRange(1,1000),
                ClosedRange(1,1000),
                (l,r) => (l,r,1000-l-r),
                (l,r) => r > l && r+l > r
            )                                                                                       // filter out tuples with negative values
            .Where(tup => tup.Item1.Squared() + tup.Item2.Squared() == tup.Item3.Squared())         // keep only valid pythagorean triples
            .Select(tup => tup.Item1 * tup.Item2 * tup.Item3)                                       // get product of tuple
            .First()                                                                                // get first product
            .ConsoleWriteLine();                                                                    
        }
    }
}
