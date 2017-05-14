using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AF.UnityUtilities
{
    public class CircularBuffer<T>
    {
        T[] _items;
        int _count;
        int _currentPosition;

        public CircularBuffer(int maxSize)
        {
            _items = new T[maxSize];
            _count = 0;
            _currentPosition = 0;
        }

        public void Add(T item)
        {
            _items[_currentPosition] = item;
            _currentPosition = (_currentPosition + 1) % _items.Length;
            if (_count < _items.Length)
            {
                _count++;
            }
        }

        public int Count { get { return _count; } }

        public T Get(int index)
        {
            if (index < 0) throw new InvalidOperationException();
            if (index > _count - 1) throw new IndexOutOfRangeException();

            return _items[(_currentPosition + (_items.Length - index - 1)) % _items.Length];
        }

        public T GetAtOrLast(int index)
        {
            index = Mathf.Min(Count - 1, index);
            return Get(index);
        }

        public T GetAtOrFirst(int index)
        {
            index = index > (Count - 1) ? 0 : index;
            return Get(index);
        }
    }
}