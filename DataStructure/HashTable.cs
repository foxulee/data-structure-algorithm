using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure
{
    public class HashTable<T>
    {
        private readonly int _size;
        private int[] _slots;
        private T[] _data;
        private int _count;
        public HashTable(int size)
        {
            _size = size;
            _count = 0;
            _slots = new int[_size];
            _data = new T[_size];
        }

        public int Count => _count;

        public void Put(int key, T value)
        {
            if (Count >= _size)
            {
                throw new Exception("Hash Table is full!");
            }
            int hashCode = GetArrayPosition(key);
            //if the slot is empty
            if (_slots[hashCode] == 0)
            {
                _slots[hashCode] = key;
                _data[hashCode] = value;
            }
            else // slot is not empty
            {
                // if the key in the slot == key, modified the value
                if (_slots[hashCode] == key) _data[hashCode] = value;
                // if _slots[hashCode] != key, cause key collision, need to find next available position
                else
                {
                    int newHashCode = GetReHashedPosition(hashCode);
                    while (_slots[newHashCode] != 0 && _slots[newHashCode] != key) //either empty or existing
                        newHashCode = GetReHashedPosition(newHashCode);

                    //after find the new position, insert or update the key and value
                    if (_slots[newHashCode] == 0)
                    {
                        _slots[newHashCode] = key;
                        _data[newHashCode] = value;
                    }
                    else _data[newHashCode] = value;
                }
            }
            _count++;
        }

        private int GetArrayPosition(int key)
        {
            return Math.Abs(key % _size);
        }

        private int GetReHashedPosition(int hashCode)
        {
            return (hashCode + 1) % _size;
        }

        /// <summary>
        /// Best: O(1), Worst: O(n)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(int key)
        {
            int hashCode = GetArrayPosition(key);
            //if found
            if (_slots[hashCode] == key) return _data[hashCode];

            //if current !=key, maybe collision, rehash and compare
            int newHashCode = GetReHashedPosition(hashCode);
            while (_slots[newHashCode] != key)
            {
                if (newHashCode == hashCode) return default(T);
                newHashCode = GetReHashedPosition(newHashCode);
            }
            return _data[newHashCode];
        }

        public void Delete(int key)
        {
            if (Count == 0) throw new Exception("The Hash Table is empty");
            int hashCode = GetArrayPosition(key);
            //if found
            if (_slots[hashCode] == key)
            {
                _slots[hashCode] = 0;
                _data[hashCode] = default(T);
                return;
            }

            //if current !=key, maybe collision, rehash and compare
            int newHashCode = GetReHashedPosition(hashCode);
            while (_slots[newHashCode] != key)
            {
                if (newHashCode == hashCode) throw new Exception("Not found!");
                newHashCode = GetReHashedPosition(newHashCode);
            }
            _slots[newHashCode] = 0;
            _data[newHashCode] = default(T);
        }

        public bool Contains(int key)
        {
            return Get(key) != null;
        }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T this[int key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        /// <summary>
        /// iteration
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var data in _data)
            {
                yield return data;
            }
        }
    }
}