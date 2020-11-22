using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Euler
{
    static class TestSupport
    {
        public static TestCaseData ToTestCaseData<TResult>(EulerProblemInstance<TResult> instance)
        {
            var ret = new TestCaseData(instance);

            ret = new TestCaseData((Func<object>)(() => instance.Execute()));
            ret.TestName = $"Euler{instance.ProblemNumber:000}.{instance.Method}({instance.ParameterRepresentation})";
            ret.ExpectedResult = instance.ExpectedResult;

            if (instance.IsCanonical) ret.Properties.Add("Category", "Canonical");

            if (instance.IsFull) ret.Properties.Add("Category", "Full");
            else ret.Properties.Add("Category", "Mini");

            return ret;
        }
    }
}
