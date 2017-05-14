using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AF.UnityUtilities
{
    public class Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }

        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public bool Equals(Pair<T1, T2> other)
        {
            if (other == null) return false;
            return First.Equals(other.First) &&
                    Second.Equals(other.Second);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Pair<T1, T2>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ First.GetHashCode();
                hashCode = (hashCode * 397) ^ Second.GetHashCode();
                return hashCode;
            }
        }
    }

    public class Tuple3<T1, T2, T3> : IEquatable<Tuple3<T1, T2, T3>>
    {
        Pair<T1, Pair<T2, T3>> items;
        public T1 First { get { return items.First; } }
        public T2 Second { get { return items.Second.First; } }
        public T3 Third { get { return items.Second.Second; } }

        public Tuple3(T1 first, T2 second, T3 third)
        {
            items = new Pair<T1, Pair<T2, T3>>(first, new Pair<T2, T3>(second, third));
        }

        public bool Equals(Tuple3<T1, T2, T3> other)
        {
            return Equals(items, other.items);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Pair<T1, T2>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ items.GetHashCode();
                return hashCode;
            }
        }
    }
}
