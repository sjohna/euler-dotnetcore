using System.Collections.Generic;
using System;

namespace Euler
{
    public static class Sequence
    {
        public static IEnumerable<long> Fibonacci() {
            long curr = 1;
            long prev = 0;

            while(true) {
                yield return curr;
                long temp = prev;
                prev = curr;
                curr += temp;
            }
        }

        public static IEnumerable<long> CountFrom(long n, long by=1) 
        {
            while(true) 
            {
                yield return n;
                n += by;
            }
        }

        public static IEnumerable<long> ClosedRange(long min, long max)
        {
            for (long num = min; num <= max; ++num)
            {
                yield return num;
            }
        }
    }
}