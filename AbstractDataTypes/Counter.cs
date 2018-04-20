using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDataTypes
{
    class Counter
    {
        private readonly string _name = null;
        private int _value = 0;
        public Counter(string str)
        {
            this._name = str;
        }

        public void Increment()
        {
            this._value++;
        }

        public int GetCurrentValue()
        {
            return this._value;
        }

        public override string ToString()
        {
			return this._name + ": " + this._value;
        }
    }
}
