using System.Linq;
using System.Collections.Generic;
using System.Text;
using static Euler.TestSupport;
using NUnit.Framework;
using System;

namespace EulerProblemTest
{
    class ProblemTests
    {
        static IEnumerable<TestCaseData> Euler001TestCases() => Euler1.Program.ProblemInstances.Select(instance => ToTestCaseData(instance));

        [TestCaseSource(nameof(Euler001TestCases))]
        public object TestProblemInstance(Func<object> testFunction) => testFunction();
    }
}
