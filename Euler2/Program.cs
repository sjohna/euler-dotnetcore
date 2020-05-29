using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler2
{
    class Program
    {
        static IEnumerable<int> Fibonacci() {
            int curr = 1;
            int prev = 0;

            while(true) {
                yield return curr;
                int temp = prev;
                prev = curr;
                curr += temp;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Fibonacci().TakeWhile(n => n < 4000000).Where(n => n % 2 == 0).Sum());
        }
    }
}
