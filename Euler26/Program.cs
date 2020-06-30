﻿using System;
using System.Collections.Generic;
using System.Linq;

using static Euler.Extension;
using static Euler.Sequence;

namespace Euler26
{
    public struct LongDivisionStep
    {
        public long Dividend {get;}
        public long Divisor {get;}

        public LongDivisionStep(long dividend, long divisor)
        {
            this.Dividend = dividend;
            this.Divisor = divisor;
        }
    }

    class Program
    {
        public static int CycleLength(long dividend, long divisor)
        {
            return CycleLength(new List<LongDivisionStep>(), new LongDivisionStep(dividend, divisor));
        }

        public static int CycleLength(List<LongDivisionStep> history, LongDivisionStep state)
        {
            if (state.Dividend == 0) return 0;
            if (history.IndexOf(state) != -1) 
            {
                int index = history.IndexOf(state);

                return history.Count - index;
            }

            history.Add(state);

            if(state.Dividend < state.Divisor)
            {
                return CycleLength(history, new LongDivisionStep(dividend: state.Dividend*10, divisor: state.Divisor));
            }

            return CycleLength(history, new LongDivisionStep(dividend: (state.Dividend % state.Divisor)*10, divisor: state.Divisor));
        }

        static void Main(string[] args)
        {
            ClosedRange(1,1000).Select(n => (n, CycleLength(1,n))).OrderByDescending(c => c.Item2).First().ConsoleWriteLine();
        }
    }
}
