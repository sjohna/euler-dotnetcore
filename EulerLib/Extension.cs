using System;
using System.Collections.Generic;

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
    }
}