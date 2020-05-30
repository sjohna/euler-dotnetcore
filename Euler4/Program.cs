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
            CrossSelect(ClosedRange(100,999), ClosedRange(100,999), (x,y) => x*y)
            .Where(n => n.IsPalindromeNumber())
            .Max()
            .ConsoleWriteLine();
        }
    }
}
