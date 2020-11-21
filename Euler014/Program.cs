using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;

namespace Euler14
{
    class Program
    {
        static void Main(string[] args)
        {
            ClosedRange(1,1000000)
            .Select(n => (n, (long)(Collatz(n).Count())))
            .Aggregate((a, b) => (a.Item2 > b.Item2) ? a : b)
            .Item1
            .ConsoleWriteLine();
        }
    }
}
