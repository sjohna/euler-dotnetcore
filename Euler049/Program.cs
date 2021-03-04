using System;
using System.Linq;

using static Euler.Sequence;
using static Euler.Extension;
using Euler;
using System.Collections.Generic;

namespace Euler49
{
    public class Program
    {
        static void Main(string[] args)
        {
            FunctionChain().ConsoleWriteLine();
        }

        public static string FunctionChain()
        {
            return Primes().SkipWhile(p => p < 1000).TakeWhile(p => p < 10000)                      // four digit primes
                .GroupBy(p => String.Join("", p.ToString().OrderBy(c => c)))                        // grouped into those that are permutations (by comparing the digits, sorted)
                .Select(g => g.OrderBy(p => p))                                                     // put each group in order
                .Select(g => g.SubsetsOfSize(3))                                                    // select three element subsets of each group of permutations
                .Flatten()                                                                          // flatten the list of lists three-element lists into a list of three-element lists
                .Where(g => g.ElementAt(1) - g.ElementAt(0) == g.ElementAt(2) - g.ElementAt(1))     // filter for only those three element lists that are arithmetic progressions
                .Select(g => String.Join("", g))                                                    // join the three numbers into a string
                .Where(p => p != "148748178147")                                                    // filter out the solution given in the problem description...
                .First();                                                                           // leaving the other solution
        }

        public static IEnumerable<EulerProblemInstance<string>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<string>.NoParameterInstanceFactory(typeof(Euler49.Program), 49, "296962999629");

                yield return factory(nameof(FunctionChain)).Canonical();
            }
        }
    }
}
