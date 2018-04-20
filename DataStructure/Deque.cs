using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStructure
{
    /// <summary>
    /// The deque is a data structure that allows you to add and remove elements at both ends of a queue. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Deque<T>
    {
        private int _count;
        private List<T> _list;
        public Deque()
        {
            _count = 0;
            _list = new List<T>();
        }

        public int Count => _count;

        public void AddFront(T t)
        {
            _list.Add(t);
            _count++;
        }

        public void AddRear(T t)
        {
            _list.Insert(0,t);
            _count++;
        }

        public T RemoveFront()
        {
            if (!IsEmpty())
            {
                T result = _list[_count - 1];
                _count--;
                return result;
            }
            else throw new Exception("The Deque Is Empty");
        }

        public T RemoveRear()
        {
            if (!IsEmpty())
            {
                T result = _list[0];
                _count--;
                return result;
            }
            else throw new Exception("The Deque Is Empty");
        }

        public T PeekFront()
        {
            return _list[_count - 1];
        }

        public T PeekRear()
        {
            return _list[_count - 1];
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public override string ToString()
        {
            string strs = "";
            for (int i = 0; i < _list.Count; i++)
            {
                strs += _list[i];
                if (i != Count-1)
                {
                    strs += ", ";
                }
            }
            return strs;
        }
    }
}
