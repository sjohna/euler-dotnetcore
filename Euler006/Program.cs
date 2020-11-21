using System;
using System.Linq;

namespace Euler6
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int,int> square = x => x*x;
            Console.WriteLine(square(Enumerable.Range(1,100).Sum()) - Enumerable.Range(1,100).Select(x => square(x)).Sum());
        }
    }
}
