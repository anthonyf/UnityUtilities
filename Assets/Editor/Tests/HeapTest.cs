using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AF.UnityUtilities
{
    public class HeapTest
    {

        [Test]
        public void MaxHeapTest()
        {

            Heap<int> heap = new Heap<int>(new FunctionalComparer<int>((a, b) => a.CompareTo(b)));

            heap.Add(1);
            heap.Add(4);
            heap.Add(5);
            heap.Add(2);
            heap.Add(3);

            heap.UpdateItem(3);

            Assert.AreEqual(heap.RemoveFirst(), 5);
            Assert.AreEqual(heap.RemoveFirst(), 4);
            Assert.AreEqual(heap.RemoveFirst(), 3);
            Assert.AreEqual(heap.RemoveFirst(), 2);
            Assert.AreEqual(heap.RemoveFirst(), 1);
        }

        [Test]
        public void MinHeapTest()
        {

            Heap<int> heap = new Heap<int>(new FunctionalComparer<int>((a, b) => -a.CompareTo(b)));

            heap.Add(1);
            heap.Add(4);
            heap.Add(5);
            heap.Add(2);
            heap.Add(3);

            heap.UpdateItem(3);

            Assert.AreEqual(heap.RemoveFirst(), 1);
            Assert.AreEqual(heap.RemoveFirst(), 2);
            Assert.AreEqual(heap.RemoveFirst(), 3);
            Assert.AreEqual(heap.RemoveFirst(), 4);
            Assert.AreEqual(heap.RemoveFirst(), 5);
        }
    }
}