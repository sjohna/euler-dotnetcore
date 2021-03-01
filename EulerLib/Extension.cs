using System;
using System.Collections.Generic;
using System.Numerics;

namespace Euler
{
    public static class Extension
    {
        public static IEnumerable<T> Yield<T>(this T item) {
            yield return item;
        }

        public static void ConsoleWriteLine(this object o) 
        {
            Console.WriteLine(o);
        }

        // This will fail for some non-ASCII strings...
        public static string Reverse(this string s) 
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static bool IsPalindromeNumber(this long number) 
        {
            return number.ToString().Reverse() == number.ToString();
        }

        public static long Squared(this long number)
        {
            return checked(number * number);
        }

        public static TResult ApplyFunction<TInput, TResult>(this TInput input, Func<TInput, TResult> func)
        {
            return func(input);
        }

        public static BigInteger IntegerSquareRoot(this BigInteger value)
        {
            var rangeMin = new BigInteger(1);
            var rangeMax = value;
            var average = (rangeMin + rangeMax) / 2;

            while (rangeMin != average)
            {
                var averageSquared = average * average;

                if (averageSquared <= value)
                {
                    rangeMin = average;
                }
                else
                {
                    rangeMax = average;
                }

                average = (rangeMin + rangeMax) / 2;
            }

            return rangeMin;
        }

        public static bool IsSquare(this BigInteger value)
        {
            return BigInteger.Pow(value.IntegerSquareRoot(), 2) == value;
        }
    }
}