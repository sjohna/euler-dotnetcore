using System;
using System.Linq;
using static Euler.Sequence;
using static Euler.Extension;

namespace Euler2
{
    class Program
    {
        static void Main(string[] args)
        {
            Fibonacci().TakeWhile(n => n < 4000000).Where(n => n % 2 == 0).Sum().ConsoleWriteLine();
        }
    }
}
