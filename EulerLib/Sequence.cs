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

        public static IEnumerable<long> Collatz(long start)
        {
            long num = start;
            while (num != 1) {
                yield return num;
                num = num % 2 == 0 ? num/2 : num * 3 + 1;
            }

            yield return 1;
        }

        public static IEnumerable<TAccumulate> PartialAggregates<TSource, TAccumulate>(this IEnumerable<TSource> sequence, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> aggregateFunc)
        {
            TAccumulate soFar = seed;
            foreach (var item in sequence)
            {
                soFar = aggregateFunc(soFar, item);
                yield return soFar;
            }
        }

        public static IEnumerable<long> PartialSums(this IEnumerable<long> sequence)
        {
            return sequence.PartialAggregates(0L, (long a, long b) => a + b);
        }

        public static IEnumerable<(int index, TResult value)> SelectWithIndices<TSource, TResult>(this IEnumerable<TSource> sequence, Func<TSource, TResult> selectFunc)
        {
            return sequence.Select((item, index) => (index: index, value: selectFunc(item)));
        }
    }
}