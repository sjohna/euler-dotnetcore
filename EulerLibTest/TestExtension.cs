using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;

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
        public void IntegerSquareRoot(string valueRepresentation, string rootRepresentation)
        {
            var value = BigInteger.Parse(valueRepresentation);
            var root = BigInteger.Parse(rootRepresentation);

            Assert.AreEqual(root, value.IntegerSquareRoot());
        }

        [Test]
        public void SubSetsOfSize_SizeOneInSingleElementEnumerable()
        {
            var list = new int[] { 1 };

            var subsets = list.SubsetsOfSize(1).ToList();

            Assert.AreEqual(1, subsets.Count());
            Assert.AreEqual(1, subsets[0].First());
            Assert.AreEqual(1, subsets[0].Count());
        }

        [Test]
        public void SubSetsOfSize_SizeOneInMultipleElementEnumerable()
        {
            var list = new int[] { 1, 2, 3 };

            var subsets = list.SubsetsOfSize(1).ToList();

            Assert.AreEqual(3, subsets.Count());
            Assert.AreEqual(1, subsets[0].First());
            Assert.AreEqual(2, subsets[1].First());
            Assert.AreEqual(3, subsets[2].First());

            Assert.AreEqual(1, subsets[0].Count());
            Assert.AreEqual(1, subsets[1].Count());
            Assert.AreEqual(1, subsets[2].Count());
        }

        [Test]
        public void SubSetsOfSize_SizeTwoInMultipleElementEnumerable()
        {
            var list = new int[] { 1, 2, 3 };

            var subsets = list.SubsetsOfSize(2).ToList();

            Assert.AreEqual(3, subsets.Count());

            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 1, 2 }, subsets[0]));
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 1, 3 }, subsets[1]));
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 2, 3 }, subsets[2]));
        }

        [Test]
        public void SubSetsOfSize_SizeEqualToEnumerableLength()
        {
            var list = new int[] { 1, 2, 3 };

            var subsets = list.SubsetsOfSize(3).ToList();

            Assert.AreEqual(1, subsets.Count());

            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 1, 2, 3 }, subsets[0]));
        }

        [Test]
        public void SubSetsOfSize_SizeGreateThanEnumerable()
        {
            var list = new int[] { 1, 2, 3 };

            var subsets = list.SubsetsOfSize(4).ToList();

            Assert.AreEqual(0, subsets.Count());
        }
    }
}
