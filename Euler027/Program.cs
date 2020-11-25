using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using static Euler.Mathematical;
using System.Collections.Generic;
using Euler;

namespace Euler27
{
    // TODO: think I can refactor this one as well...
    public class Program
    {
        public static void Main(string[] args)
        {
            CrossSelect(
                ClosedRange(-999,999),
                ClosedRange(-1000,1000),
                (a, b) => (a, b)
            )
            .Select(
                coeffs => 
                (
                    coeffs: coeffs,
                    numPrimes: CountFrom(0).Select(n => n.Squared() + coeffs.a * n + coeffs.b).TakeWhile(n => IsPrime(n)).Count()
                )
            )
            .Aggregate(
                (a, b) => a.numPrimes > b.numPrimes ? a : b
            )
            .ApplyFunction(tup => tup.coeffs.a * tup.coeffs.b)
            .ConsoleWriteLine();  
        }

        public static long SimpleCrossSelectWithNaiveBounds()
        {
            return CrossSelect(
                ClosedRange(-999, 999),
                ClosedRange(-1000, 1000),
                (a, b) => (a, b)
            )
            .Select(
                coeffs =>
                (
                    coeffs: coeffs,
                    numPrimes: CountFrom(0).Select(n => n.Squared() + coeffs.a * n + coeffs.b).TakeWhile(n => IsPrime(n)).Count()
                )
            )
            .Aggregate(
                (a, b) => a.numPrimes > b.numPrimes ? a : b
            )
            .ApplyFunction(tup => tup.coeffs.a * tup.coeffs.b);
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.NoParameterInstanceFactory(typeof(Euler27.Program), 27, -59231L);

                yield return factory(nameof(SimpleCrossSelectWithNaiveBounds)).Canonical();
            }
        }
    }
}
