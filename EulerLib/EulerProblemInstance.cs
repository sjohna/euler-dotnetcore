﻿using System;
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
                    Execute: () => (TResult) problemType.GetMethod(methodName).Invoke(null, new object[] { parameter }));

        public EulerProblemInstance(
            int ProblemNumber,
            string Method,
            string ParameterRepresentation,
            TResult ExpectedResult,
            Func<TResult> Execute,
            bool IsFull = true,
            bool IsCanonical = false
            )
        {
            this.ProblemNumber = ProblemNumber;
            this.Method = Method;
            this.ParameterRepresentation = ParameterRepresentation;
            this.ExpectedResult = ExpectedResult;
            this.execute = Execute;
            this.IsFull = IsFull;
            this.IsCanonical = IsCanonical;
        }

        // fluent-style methods for setting Canonical and Full/Mini
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

        public int ProblemNumber { get; private set; }

        public string Method { get; private set; }

        public string ParameterRepresentation { get; private set; }

        public TResult ExpectedResult { get; private set; }

        private Func<TResult> execute;

        public TResult Execute() => execute();

        public bool IsCanonical { get; private set; }

        public bool IsFull { get; private set; }
    }
}
