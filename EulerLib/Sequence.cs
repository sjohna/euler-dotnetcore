using System.Collections.Generic;
using System.Linq;
using System;

using static Euler.Mathematical;

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

        public static IEnumerable<long> Primes() 
        {
            return CountFrom(3,2).Where(n => IsPrime(n)).Prepend(2L);
        }

        public static IEnumerable<TResult> CrossSelect<TLeft,TRight,TResult>(
            IEnumerable<TLeft> left, 
            IEnumerable<TRight> right, 
            Func<TLeft,TRight,TResult> selectFunc,
            Func<TLeft,TRight, bool> filterFunc = null) 
        {
            filterFunc = filterFunc ?? ((TLeft l, TRight r) => true);

            return from leftValue in left
                   from rightValue in right
                   where filterFunc(leftValue,rightValue)
                   select selectFunc(leftValue,rightValue);
        }
    }
}