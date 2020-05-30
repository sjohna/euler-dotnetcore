using System;
using System.Linq;

namespace Euler4
{
    static class Program
    {
        static void ConsoleWriteLine(this object o) 
        {
            Console.WriteLine(o);
        }

        // This will fail for some non-ASCII strings...
        static string Reverse(this string s) 
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static bool IsPalindromeNumber(this int number) 
        {
            return number.ToString().Reverse() == number.ToString();
        }

        static void Main(string[] args)
        {
            (
            from x in Enumerable.Range(100,899)
            from y in Enumerable.Range(100,899)
            where (x*y).IsPalindromeNumber()
            select x * y
            )
            .Max()
            .ConsoleWriteLine();
        }
    }
}
