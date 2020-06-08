using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Euler
{

    public class MinHeap<T> : IEnumerable<T> where T : IComparable<T>
    {
        private T[] m_data;
        private int m_count;

        public MinHeap(int size = 10)
        {
            m_data = new T[size];
            m_count = 0;
        }

        public void Add(T newItem)
        {
            if(m_data.Length == Count)
            {
                var newArray = new T[m_data.Length * 2];
                Array.Copy(m_data, newArray, m_data.Length);
                m_data = newArray;
            }

            m_data[Count] = newItem;
            HeapifyUp(Count);
            ++m_count;
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Add(item);
            }
        }

        private int ParentOf(int index) =>
            (index - 1)/2;

        private void HeapifyUp(int index)
        {
            if(index == 0) return;

            int parent = ParentOf(index);
            if (m_data[parent].CompareTo(m_data[index]) > 0)
            {
                T tmp = m_data[parent];
                m_data[parent] = m_data[index];
                m_data[index] = tmp;
            }
            HeapifyUp(parent);
        }

        private void HeapifyDown(int index)
        {
            int firstChild = index*2+1;
            if(m_count <= firstChild) return;

            int secondChild = index*2+2;
            int minChildIndex = firstChild;

            if (secondChild < m_count && m_data[firstChild].CompareTo(m_data[secondChild]) > 0)
            {
                minChildIndex = secondChild;
            }

            if(m_data[minChildIndex].CompareTo(m_data[index]) < 0)
            {
                T tmp = m_data[index];
                m_data[index] = m_data[minChildIndex];
                m_data[minChildIndex] = tmp;
                HeapifyDown(minChildIndex);
            }
        }

        public T Pop()
        {
            var ret = m_data[0];
            --m_count;
            m_data[0] = m_data[Count];
            m_data[Count] = default(T);
            HeapifyDown(0);
            return ret;
        }

        public int Count => m_count;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_count; ++i) yield return m_data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < m_count; ++i) yield return m_data[i];
        }
    }

}
