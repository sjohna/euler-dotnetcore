using System;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;
using static Euler.Mathematical;
using System.Collections.Generic;
using Euler;

namespace Euler50
{
    public class Program
    {
        static void Main(string[] args)
        {
            var primesUnderLimit = Primes().TakeWhile(p => p < 1000000).ToList();       // get all primes under limit as a list, to avoid recomputing

            ClosedRange(0,primesUnderLimit.Count() - 1)                                 // range of indices within prime list to start sums
            .Select(n => primesUnderLimit.Skip((int)n))                                 // get sequence of primes starting at that index
            .Select
            (                                                                           // for each of these sequences of primes
                pSeq => pSeq                                                            
                .PartialSums()                                                          // convert to sequence of sums of partial sums
                .Select((pSum,index) => (numSummands: index + 1, pSum: pSum))           // associate number of summands with each sum
                .TakeWhile(val => val.pSum < 1000000)                                   // skip sums greater than limit
                .Where(val => IsPrime(val.pSum))                                        // filter to only prime sums
                .Aggregate((a,b) => a.numSummands > b.numSummands ? a : b)              // select prime with max number of summands from this starting point
            )
            .Aggregate((a,b) => a.numSummands > b.numSummands ? a : b)                  // select prime with max number of summands across all starting points
            .ConsoleWriteLine();
        }

        public static long FunctionChainsToGeneratePimeSequences(long upTo)
        {
            var primesUnderLimit = Primes().TakeWhile(p => p < upTo).ToList();          // get all primes under limit as a list, to avoid recomputing

            return ClosedRange(0, primesUnderLimit.Count() - 1)                         // range of indices within prime list to start sums
            .Select(n => primesUnderLimit.Skip((int)n))                                 // get sequence of primes starting at that index
            .Select
            (                                                                           // for each of these sequences of primes
                pSeq => pSeq
                .PartialSums()                                                          // convert to sequence of sums of partial sums
                .Select((pSum, index) => (numSummands: index + 1, pSum: pSum))          // associate number of summands with each sum
                .TakeWhile(val => val.pSum < upTo)                                   // skip sums greater than limit
                .Where(val => IsPrime(val.pSum))                                        // filter to only prime sums
                .Aggregate((a, b) => a.numSummands > b.numSummands ? a : b)             // select prime with max number of summands from this starting point
            )
            .Aggregate((a, b) => a.numSummands > b.numSummands ? a : b)                 // select prime with max number of summands across all starting points
            .pSum;
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactory<long>(typeof(Euler50.Program), 50);

                yield return factory(nameof(FunctionChainsToGeneratePimeSequences), 1000000L, 997651L).Canonical();
                yield return factory(nameof(FunctionChainsToGeneratePimeSequences), 100L, 41L).Mini();
                yield return factory(nameof(FunctionChainsToGeneratePimeSequences), 1000L, 953L).Mini();
            }
        }
    }
}
