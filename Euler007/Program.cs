using System;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;

namespace Euler7
{
    class Program
    {
        static void Main(string[] args)
        {
            Primes().Skip(10000).First().ConsoleWriteLine();
        }
    }
}
