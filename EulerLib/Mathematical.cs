using System.Linq;
using System.Collections.Generic;

namespace Euler
{
    public static class Mathematical
    {
        public static List<long> Factorize(long num) 
        {
            var ret = new List<long>();

            var factors = Enumerable.Concat(2L.Yield(), Sequence.CountFrom(3,2));

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

        public static long GCD(long a, long b) 
        {
            if (b < a) return GCD(b,a);
            if (a == 0) return b;
            return GCD(b % a, a);
        }

        public static long LCM(long a, long b) 
        {
            return checked((a*b)/GCD(a,b));
        }

        public static bool IsPrime(long num)
        {
            return Factorize(num).Count == 1;
        }

    }
}