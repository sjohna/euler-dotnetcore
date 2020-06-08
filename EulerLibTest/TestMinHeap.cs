using NUnit.Framework;

using Euler;
using System.Linq;
using System;
using System.Collections.Generic;

namespace EulerLibTest
{
    public class TestMinHeap
    {
        private MinHeap<int> m_heap; 

        [SetUp]
        public void Setup()
        {
            m_heap = new MinHeap<int>();
        }

        [Test]
        public void TestCreation()
        {
            Assert.AreEqual(0, m_heap.Count);
        }

        [Test]
        public void TestAddSingleItem()
        {
            m_heap.Add(1);
            Assert.AreEqual(1, m_heap.Count);
        }

        [Test]
        public void TestAddTwoItems()
        {
            m_heap.Add(2);
            m_heap.Add(1);
            Assert.AreEqual(2, m_heap.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] {1,2}, m_heap));
        }

        [Test]
        public void TestPopSingleItem()
        {
            m_heap.Add(1);
            var value = m_heap.Pop();
            Assert.AreEqual(1,value);
            Assert.AreEqual(0,m_heap.Count);
        }

        [Test]
        public void TestPopTwoItems()
        {
            m_heap.Add(2);
            m_heap.Add(1);
            Assert.AreEqual(1, m_heap.Pop());
            Assert.AreEqual(1, m_heap.Count);
            Assert.AreEqual(2, m_heap.Pop());
            Assert.AreEqual(0, m_heap.Count);
        }

        [TestCase(1,2,3)]
        [TestCase(1,3,2)]
        [TestCase(2,1,3)]
        [TestCase(2,3,1)]
        [TestCase(3,1,2)]
        [TestCase(3,2,1)]
        public void TestPopThreeItems(params int[] items)
        {
            foreach (var item in items)
            {
                m_heap.Add(item);
            }

            Assert.AreEqual(3, m_heap.Count);

            Assert.AreEqual(1, m_heap.Pop());
            Assert.AreEqual(2, m_heap.Count);

            Assert.AreEqual(2, m_heap.Pop());
            Assert.AreEqual(1, m_heap.Count);

            Assert.AreEqual(3, m_heap.Pop());
            Assert.AreEqual(0, m_heap.Count);
        }

        [TestCase(1,2,3,4)]
        [TestCase(4,3,2,1)]
        [TestCase(1,3,2,4)]
        [TestCase(3,2,4,1)]
        [TestCase(2,1,3,4)]
        public void TestAddAndPopFourItems(params int[] items)
        {
            foreach (var item in items)
            {
                m_heap.Add(item);
            }


            Assert.AreEqual(4, m_heap.Count);

            Assert.AreEqual(1, m_heap.Pop());
            Assert.AreEqual(3, m_heap.Count);

            Assert.AreEqual(2, m_heap.Pop());
            Assert.AreEqual(2, m_heap.Count);

            Assert.AreEqual(3, m_heap.Pop());
            Assert.AreEqual(1, m_heap.Count);

            Assert.AreEqual(4, m_heap.Pop());
            Assert.AreEqual(0, m_heap.Count);
        }

        [Test]
        public void TestHeapGrowsBackingStorage()
        {
            m_heap = new MinHeap<int>(1);

            m_heap.Add(1);
            m_heap.Add(2);

            Assert.AreEqual(2,m_heap.Count);

            Assert.AreEqual(1, m_heap.Pop());
            Assert.AreEqual(1, m_heap.Count);
            Assert.AreEqual(2, m_heap.Pop());
            Assert.AreEqual(0, m_heap.Count);
        }

        [Test]
        [Repeat(1000)]
        public void TestRandomNumsInHeap()
        {
            var nums = Enumerable.Range(1,100).OrderBy(n => Guid.NewGuid());

            foreach (var num in nums)
            {
                m_heap.Add(num);
            }

            Assert.AreEqual(100, m_heap.Count);

            for (int expected = 1; expected <= 100; ++expected)
            {
                Assert.AreEqual(expected, m_heap.Pop());
                Assert.AreEqual(100 - expected, m_heap.Count);
            }
        }

        [Test]
        [Repeat(10)]
        public void TestRandomNumsInHeap_LargerHeap()
        {
            var nums = Enumerable.Range(1,10000).OrderBy(n => Guid.NewGuid());

            foreach (var num in nums)
            {
                m_heap.Add(num);
            }

            Assert.AreEqual(10000, m_heap.Count);

            for (int expected = 1; expected <= 10000; ++expected)
            {
                Assert.AreEqual(expected, m_heap.Pop());
                Assert.AreEqual(10000 - expected, m_heap.Count);
            }
        }
    }
}