using System;
using System.Linq;

namespace Euler1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Enumerable.Range(1,999).Where(n => n % 3 == 0 || n % 5 == 0).Sum());
        }
    }
}
