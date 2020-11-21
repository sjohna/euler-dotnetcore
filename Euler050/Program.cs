using System;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;
using static Euler.Mathematical;

namespace Euler50
{
    class Program
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
    }
}
