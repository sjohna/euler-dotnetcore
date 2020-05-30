using System;
using System.Linq;

namespace Euler5
{
    class Program
    {
        static long GCD(long a, long b) 
        {
            if (b < a) return GCD(b,a);
            if (a == 0) return b;
            return GCD(b % a, a);
        }

        static long LCM(long a, long b) 
        {
            return checked((a*b)/GCD(a,b));
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Enumerable.Range(1,20).Aggregate(1,(a,b) => (int)LCM(a,b)));
        }
    }
}
