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

        [Test]
        public void TestPairwise()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };

            var pairs = new List<Pair<int, int>>();

            list.Pairwise((a, b) =>
            {
                pairs.Add(new Pair<int,int>(a, b));
            });

            Assert.AreEqual(3, pairs.Count);
        }

        [Test]
        public void TestTriplewise()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };

            var triples = new List<Tuple3<int,int,int>>();

            list.Tripletwise((a, b, c) =>
            {
                triples.Add(new Tuple3<int, int, int>(a, b, c));
            });

            Assert.AreEqual(2, triples.Count);
            Assert.AreEqual(1, triples[0].First);
            Assert.AreEqual(2, triples[0].Second);
            Assert.AreEqual(3, triples[0].Third);
            Assert.AreEqual(4, triples[1].First);
            Assert.AreEqual(5, triples[1].Second);
            Assert.AreEqual(6, triples[1].Third);
        }
    }
}