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
        static IEnumerable<TestCaseData> AllProblemTestCases()
        {
            foreach (var instance in Euler1.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler2.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler3.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler4.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler5.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler6.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler7.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler8.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler9.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler10.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler14.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler26.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler27.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler35.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler37.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler39.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler50.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler81.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler82.Program.ProblemInstances) yield return ToTestCaseData(instance);
            foreach (var instance in Euler83.Program.ProblemInstances) yield return ToTestCaseData(instance);
        }

        [TestCaseSource(nameof(AllProblemTestCases))]
        public object TestProblemInstance(Func<object> testFunction) => testFunction();
    }
}
