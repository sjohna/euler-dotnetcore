﻿using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using System.Collections.Generic;
using Euler;

namespace Euler9
{
    public class Program
    {
        static void Main(string[] args)
        {
            CrossSelect(                                                                            // generate tuples where Item1 + Item2 + Item3 == 1000
                ClosedRange(1,1000),
                ClosedRange(1,1000),
                (a,b) => (a: a, b: b, c: 1000-a-b)
            )
            .Where(tup => tup.c > 0)
            .Where(tup => tup.b >= tup.a)                                                                     
            .Where(tup => tup.a.Squared() + tup.b.Squared() == tup.c.Squared())         // keep only valid pythagorean triples
            .Select(tup => tup.a * tup.b * tup.c)                                       // get product of tuple
            .First()                                                                    // get first product
            .ConsoleWriteLine();                                                                    
        }

        public static long SimpleCrossSelect(long sum)
        {
            return CrossSelect(                                                         // generate tuples where Item1 + Item2 + Item3 == 1000
                ClosedRange(1, sum),
                ClosedRange(1, sum),
                (a, b) => (a: a, b: b, c: sum - a - b)
            )
            .Where(tup => tup.c > 0)
            .Where(tup => tup.b >= tup.a)
            .Where(tup => tup.a.Squared() + tup.b.Squared() == tup.c.Squared())         // keep only valid pythagorean triples
            .Select(tup => tup.a * tup.b * tup.c)                                       // get product of tuple
            .First();                                                                   // get first product
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler9.Program), 9);

                yield return factory(nameof(SimpleCrossSelect), 1000L, 31875000L).Canonical();
            }
        }
    }
}
