using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Stack<T>
    {
        private int _maxSize;
        private T[] _stackArray;
        private int _top;

        public Stack(int size)
        {
            _maxSize = size;
            _stackArray = new T[_maxSize];
            _top = -1;
        }

        public void Push(T item)
        {
            if (!IsFull())
            {
                _top++;
                _stackArray[_top] = item;
            }
            else
            {
                throw new Exception("Stack Is Full!");
            }

        }
        public T Pop()
        {
            if (!IsEmpty())
            {
                return _stackArray[_top--];
            }
            throw new Exception("Stack Is Already Full!");

        }

        public T Peek()
        {
            return _stackArray[_top];
        }

        public bool IsEmpty()
        {
            return _top == -1;
        }

        public bool IsFull()
        {
            return _top == _maxSize - 1;
        }

        public override string ToString()
        {
            string strs = "";
            for (int i = 0; i <= _top; i++)
            {
                strs += _stackArray[i];
                if (i != _top)
                {
                    strs += ", ";
                }
            }
            return strs;
        }
    }
}
