using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using static Euler.Mathematical;

namespace Euler27
{
    class Program
    {
        static void Main(string[] args)
        {
            CrossSelect(
                ClosedRange(-999,999),
                ClosedRange(-999,999),
                (l, r) => (a: l, b: r)
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
    }
}
