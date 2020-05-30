using System;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;

namespace Euler4
{
    static class Program
    {
        static void Main(string[] args)
        {
            (
            from x in ClosedRange(100,999)
            from y in ClosedRange(100,999)
            where (x*y).IsPalindromeNumber()
            select x * y
            )
            .Max()
            .ConsoleWriteLine();
        }
    }
}
