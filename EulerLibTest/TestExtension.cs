using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using static Euler.Extension;

namespace EulerLibTest
{
    [TestFixture]
    public class TestExtension
    {


        [TestCase("1", "1")]
        [TestCase("2", "1")]
        [TestCase("3", "1")]
        [TestCase("4", "2")]
        [TestCase("5", "2")]
        [TestCase("6", "2")]
        [TestCase("7", "2")]
        [TestCase("8", "2")]
        [TestCase("9", "3")]
        [TestCase("10", "3")]
        [TestCase("99", "9")]
        [TestCase("100", "10")]
        [TestCase("101", "10")]
        [TestCase("4090", "63")]
        [TestCase("4096", "64")]
        [TestCase("4196", "64")]
        [TestCase("999999999999", "999999")]
        [TestCase("1000000000000", "1000000")]
        [TestCase("1000000000007", "1000000")]
        [TestCase("999999999999999999999999", "999999999999")]
        [TestCase("1000000000000000000000000", "1000000000000")]
        [TestCase("1000000000000000000000456", "1000000000000")]
        public void TestIntegerSquareRoot(string valueRepresentation, string rootRepresentation)
        {
            var value = BigInteger.Parse(valueRepresentation);
            var root = BigInteger.Parse(rootRepresentation);

            Assert.AreEqual(root, value.IntegerSquareRoot());
        }
    }
}
