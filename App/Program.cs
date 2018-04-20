using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructure;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            DataStructure.Stack<int> myStack = new DataStructure.Stack<int>(3);
            Console.WriteLine("IsEmpty" +myStack.IsEmpty());
            myStack.Push(1);
            myStack.Push(2);
            Console.WriteLine(myStack.ToString());

            Console.ReadLine();
        }
    }
}
