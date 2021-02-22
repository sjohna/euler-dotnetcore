using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using static Euler.Mathematical;
using System.Collections.Generic;
using Euler;
using System;

namespace Euler37
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BuildUp());
        }

        public static long BuildUp()
        {
            var leftTruncatablePrimes = TruncatableSet(num => LeftAppends(num));
            var rightTruncatablePrimes = TruncatableSet(num => RightAppends(num));

            leftTruncatablePrimes.IntersectWith(rightTruncatablePrimes);

            return leftTruncatablePrimes.Sum();
        }

        static HashSet<long> TruncatableSet(Func<string, IEnumerable<long>> appendFunction)
        {
            var set = new HashSet<long>();

            var frontier = new Queue<string>();
            frontier.Enqueue("2");
            frontier.Enqueue("3");
            frontier.Enqueue("5");
            frontier.Enqueue("7");

            while (frontier.Count > 0)
            {
                var prime = frontier.Dequeue();

                foreach (var next in appendFunction(prime))
                {
                    if (IsPrime(next))
                    {
                        set.Add(next);
                        frontier.Enqueue(next.ToString());
                    }
                }
            }

            return set;
        }

        static IEnumerable<long> LeftAppends(string num)
        {
            foreach (int i in new int[] { 1, 2, 3, 5, 7, 9 })   // any prime containing a 4, 6, or 8 cannot be right-truncatable, so we skip those here
            {
                yield return Convert.ToInt64(string.Concat(i, num));
            }
        }

        static IEnumerable<long> RightAppends(string num)
        {
            foreach (int i in new int[] { 1, 3, 7, 9 })     // primes of more than one digit can only end in 1, 3, 7, or 9
            {
                yield return Convert.ToInt64(string.Concat(num, i));
            }
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.NoParameterInstanceFactory(typeof(Euler37.Program), 37, 748317L);

                yield return factory(nameof(BuildUp)).Canonical();
            }
        }
    }
}
