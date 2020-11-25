using System;
using System.Collections.Generic;
using System.Text;

namespace Euler
{
    public class EulerProblemInstance<TResult>
    {
        public static Func<String, TIn, TResult, EulerProblemInstance<TResult>> InstanceFactory<TIn>(Type problemType, int problemNumber) =>
            (string methodName, TIn parameter, TResult result) =>
            new EulerProblemInstance<TResult>(
                    ProblemNumber: problemNumber,
                    Method: methodName,
                    ParameterRepresentation: parameter.ToString(),
                    ExpectedResult: result,
                    Execute: () => (TResult) problemType.GetMethod(methodName, new Type[] { typeof(TIn) }).Invoke(null, new object[] { parameter }));

        public static Func<String, TIn, string, TResult, EulerProblemInstance<TResult>> InstanceFactoryWithCustomParameterRepresentation<TIn>(Type problemType, int problemNumber) =>
            (string methodName, TIn parameter, string parameterRepresentation , TResult result) =>
            new EulerProblemInstance<TResult>(
                    ProblemNumber: problemNumber,
                    Method: methodName,
                    ParameterRepresentation: parameterRepresentation,
                    ExpectedResult: result,
                    Execute: () => (TResult)problemType.GetMethod(methodName, new Type[] { typeof(TIn) }).Invoke(null, new object[] { parameter }));

        public static Func<String, EulerProblemInstance<TResult>> NoParameterInstanceFactory(Type problemType, int problemNumber, TResult result) =>
            (string methodName) =>
            new EulerProblemInstance<TResult>(
                    ProblemNumber: problemNumber,
                    Method: methodName,
                    ParameterRepresentation: "",
                    ExpectedResult: result,
                    Execute: () => (TResult)problemType.GetMethod(methodName).Invoke(null, null));

        public EulerProblemInstance(
            int ProblemNumber,
            string Method,
            string ParameterRepresentation,
            TResult ExpectedResult,
            Func<TResult> Execute,
            bool IsFull = true,
            bool IsCanonical = false,
            bool IsSlow = false
            )
        {
            this.ProblemNumber = ProblemNumber;
            this.Method = Method;
            this.ParameterRepresentation = ParameterRepresentation;
            this.ExpectedResult = ExpectedResult;
            this.execute = Execute;
            this.IsFull = IsFull;
            this.IsCanonical = IsCanonical;
            this.IsSlow = IsSlow;
        }

        // fluent-style methods for setting properties
        public EulerProblemInstance<TResult> Canonical()
        {
            this.IsCanonical = true;
            return this;
        }

        public EulerProblemInstance<TResult> Mini()
        {
            this.IsFull = false;
            return this;
        }

        public EulerProblemInstance<TResult> Slow()
        {
            this.IsSlow = true;
            return this;
        }

        public int ProblemNumber { get; private set; }

        public string Method { get; private set; }

        public string ParameterRepresentation { get; private set; }

        public TResult ExpectedResult { get; private set; }

        private Func<TResult> execute;

        public TResult Execute() => execute();

        public bool IsCanonical { get; private set; }

        public bool IsFull { get; private set; }

        public bool IsSlow { get; private set; }
    }
}
