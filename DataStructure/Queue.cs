using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    class Queue<T>
    {
        private int _maxSize; //init the set number of slots
        private T[] _queueArray; //slots to main the data
        private int _front; // this will be the index position for the element in the front
        private int _rear; // this is also going to be the index position for the element at the back of the line
        private int _nItem; // counter to maintain the number of items in our queue

        public Queue(int size)
        {
            _maxSize = size;
            _queueArray = new T[_maxSize];
            _front = 0; // index position of the first slot of the array
            _rear = -1; // there is no item in the array yet to be considered as the last item.
            _nItem = 0;
        }
        public void EnQueue(T t)
        {
            if (!IsFull())
            {
                _rear++;
                _queueArray[_rear] = t;
                _nItem++;
            }
            else
            {
                throw new Exception("The Queue is full!");
            }

        }

        public T DeQueue()
        {
            if (!IsEmpty())
            {
                T result = _queueArray[_front];
                _front++;
                _nItem--;
                return result;
            }
            else
            {
                throw new Exception("The Queue is empty!");
            }

        }

        public T Peek()
        {
            return _queueArray[_front];
        }

        public bool IsEmpty()
        {
            return _nItem == 0;
        }

        public bool IsFull()
        {
            return _nItem == _maxSize;
        }
    }
}
