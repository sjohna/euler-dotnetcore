using System;
using System.Linq;
using System.Collections.Generic;
using static Euler.Sequence;

namespace Euler
{
    public static class Mathematical
    {
        public static List<long> Factorize(long num) 
        {
            var ret = new List<long>();

            foreach (var factor in CountFrom(3,2).Prepend(2L)) 
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
            foreach (var factor in CountFrom(3,2).Prepend(2L))
            {
                if (factor > Math.Sqrt(num)) return true;
                if (num % factor == 0) return false;
            }

            return true;    // never hit
        }
    }
}