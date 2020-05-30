using System.Linq;

using static Euler.Extension;
using static Euler.Mathematical;
using static Euler.Sequence;

namespace Euler5
{
    class Program
    {
        static void Main(string[] args)
        {
            ClosedRange(1,20).Aggregate((a,b) => LCM(a,b)).ConsoleWriteLine();
        }
    }
}
