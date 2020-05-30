using System;
using System.Linq;

using static Euler.Mathematical;
using static Euler.Extension;

namespace Euler3
{
    static class Program
    {
        static void Main(string[] args)
        {
            Factorize(600851475143).Max().ConsoleWriteLine();
        }
    }
}
