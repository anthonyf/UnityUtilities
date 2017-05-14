using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using AF.UnityUtilities;

namespace AF.UnityUtilities
{

    public class UnityExtensionsTests
    {

        [Test]
        public void Vector3RoundTest()
        {
            var v = new Vector3(450, 23.8772f, .1f);
            var v2 = v.Round(.5f);

            Assert.AreEqual(new Vector3(450, 24, 0), v2);

            v2 = v.Round(100f);
            Assert.AreEqual(new Vector3(400, 0, 0), v2);

            v = new Vector3(455, 23.8772f, .1f);
            v2 = v.Round(100f);
            Assert.AreEqual(new Vector3(500, 0, 0), v2);
        }

        [Test]
        public void TestAllPairs()
        {
            var pairs = new List<Pair<int, int>>();
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.AllPairs((a, b) =>
            {
                pairs.Add(new Pair<int, int>(a, b));
            });

            Assert.AreEqual(pairs[0], new Pair<int, int>(1, 2));
            Assert.AreEqual(pairs[1], new Pair<int, int>(1, 3));
            Assert.AreEqual(pairs[2], new Pair<int, int>(1, 4));
            Assert.AreEqual(pairs[3], new Pair<int, int>(2, 3));
            Assert.AreEqual(pairs[4], new Pair<int, int>(2, 4));
            Assert.AreEqual(pairs[5], new Pair<int, int>(3, 4));
        }
    }
}