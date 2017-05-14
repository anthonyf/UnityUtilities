using System;
using System.Collections.Generic;

namespace AF.UnityUtilities
{
    public class Heap<T> : IHeap<T>
    {
        List<T> _items;
        Dictionary<T, int> _itemIndexes;
        IComparer<T> _comparer;

        public Heap(IComparer<T> comparer)
        {
            _itemIndexes = new Dictionary<T, int>();
            _items = new List<T>();
            _comparer = comparer;
        }

        public Heap(int initialSize)
        {
            _itemIndexes = new Dictionary<T, int>(initialSize);
            _items = new List<T>(initialSize);
        }

        public void Add(T item)
        {
            _itemIndexes[item] = _items.Count;
            _items.Add(item);
            SortUp(item);
        }

        public int Count { get { return _items.Count; } }

        public bool IsEmpty()
        {
            return _items.Count == 0;
        }

        public T RemoveFirst()
        {
            T first = _items[0];
            _items[0] = _items[_items.Count - 1];
            _itemIndexes[_items[0]] = 0;
            SortDown(_items[0]);
            _items.RemoveAt(_items.Count - 1);
            _itemIndexes.Remove(first);
            return first;
        }

        public void UpdateItem(T item)
        {
            SortUp(item);
            SortDown(item);
        }

        public bool Contains(T item)
        {
            return _itemIndexes.ContainsKey(item)
                && Equals(_items[_itemIndexes[item]], item);
        }

        private void SortUp(T item)
        {
            int parentIndex = (_itemIndexes[item] - 1) / 2;
            while (true)
            {
                T parentItem = _items[parentIndex];
                if (_comparer.Compare(item, parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
                parentIndex = (_itemIndexes[item] - 1) / 2;
            }
        }

        private void SortDown(T item)
        {
            while (true)
            {
                int leftChildIndex = _itemIndexes[item] * 2 + 1;
                int rightChildIndex = _itemIndexes[item] * 2 + 2;
                int swapIndex = 0;

                if (leftChildIndex < _items.Count)
                {
                    swapIndex = leftChildIndex;

                    if (rightChildIndex < _items.Count)
                    {
                        if (_comparer.Compare(_items[leftChildIndex], _items[rightChildIndex]) < 0)
                        {
                            swapIndex = rightChildIndex;
                        }
                    }

                    if (_comparer.Compare(item, _items[swapIndex]) < 0)
                    {
                        Swap(item, _items[swapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void Swap(T a, T b)
        {
            _items[_itemIndexes[a]] = b;
            _items[_itemIndexes[b]] = a;
            int itemAIndex = _itemIndexes[a];
            _itemIndexes[a] = _itemIndexes[b];
            _itemIndexes[b] = itemAIndex;
        }
    }

    public interface IHeap<T>
    {
        void Add(T item);
        bool Contains(T item);
        int Count { get; }
        bool IsEmpty();
        T RemoveFirst();
        void UpdateItem(T item);
    }
}

