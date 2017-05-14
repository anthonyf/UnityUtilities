using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

namespace AF.UnityUtilities
{

    public class CircularBufferTests
    {

        [Test]
        public void AddTest()
        {
            var cb = new CircularBuffer<int>(3);

            cb.Add(1);
            cb.Add(2);
            cb.Add(3);

            Assert.AreEqual(3, cb.Get(0));
            Assert.AreEqual(2, cb.Get(1));
            Assert.AreEqual(1, cb.Get(2));

            cb.Add(4);
            Assert.AreEqual(4, cb.Get(0));
            Assert.AreEqual(3, cb.Get(1));
            Assert.AreEqual(2, cb.Get(2));
        }
        [Test]
        public void GetTest()
        {
            var cb = new CircularBuffer<int>(3);

            cb.Add(1);
            cb.Add(2);

            Assert.Catch<IndexOutOfRangeException>(() => cb.Get(2));

            cb.Add(3);
            Assert.AreEqual(1, cb.Get(2));

            Assert.Catch<IndexOutOfRangeException>(() => cb.Get(3));
        }
    }
}