using System;
using System.Linq;
using System.Collections.Generic;

namespace Euler3
{
    static class Program
    {
        static IEnumerable<T> Yield<T>(this T item) {
            yield return item;
        }

        static IEnumerable<long> CountFrom(long n, long by=1) 
        {
            while(true) 
            {
                yield return n;
                n += by;
            }
        }

        static List<long> Factorize(long num) 
        {
            var ret = new List<long>();

            var factors = Enumerable.Concat(2L.Yield(), CountFrom(3,2));

            foreach (var factor in factors) 
            {
                if(factor > num) break;

                while (num > 0 && num % factor == 0) 
                {
                    ret.Add(factor);
                    num /= factor;
                }
            }

            return ret;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Factorize(600851475143).Max());
            //Console.WriteLine(Factorize(12).Max());
        }
    }
}
