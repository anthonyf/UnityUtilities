using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using AF.UnityUtilities;

namespace AF.UnityUtilities
{
    public class TupleTest
    {

        [Test]
        public void TupleTest1()
        {
            var t1 = new Tuple3<Vector3, Vector3, float>(new Vector3(1, 2, 3), new Vector3(1, 2, 3), .5f);
            var t2 = new Tuple3<Vector3, Vector3, float>(new Vector3(1, 2, 3), new Vector3(1, 2, 3), .5f);
            var t3 = new Tuple3<Vector3, Vector3, float>(new Vector3(1f, 2, 3), new Vector3(1, 2, 3), .5f);
            var t4 = new Tuple3<Vector3, Vector3, float>(new Vector3(2f, 2, 3), new Vector3(1, 2, 3), .5f);
            Assert.AreEqual(t1, t2);
            Assert.AreEqual(t1.GetHashCode(), t2.GetHashCode());
            Assert.AreEqual(t1.GetHashCode(), t3.GetHashCode());
            Assert.AreNotEqual(t1.GetHashCode(), t4.GetHashCode());
            Assert.AreNotEqual(t1, t4);
        }
    }
}
