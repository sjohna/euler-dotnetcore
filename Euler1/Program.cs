using System;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;

namespace Euler1
{
    class Program
    {
        static void Main(string[] args)
        {
            ClosedRange(1,999).Where(n => n % 3 == 0 || n % 5 == 0).Sum().ConsoleWriteLine();
        }
    }
}
