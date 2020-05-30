using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;

namespace Euler10
{
    class Program
    {
        static void Main(string[] args)
        {
            Primes().TakeWhile(p => p < 2000000).Sum().ConsoleWriteLine();
        }
    }
}
