using System;
using System.Diagnostics.CodeAnalysis;

namespace Employee
{
    abstract class BaseEmployee : IComparable<BaseEmployee>
    {
        protected string _name;
        protected int _age;
        protected double _payment;

        public string Name { get { return _name; } }
        public int Age { get { return _age; } }

        public BaseEmployee(string Name, int Age, double Payment)
        {
            this._name = Name;
            this._age = Age;
            this._payment = Payment;
        }
        public abstract double Calculate();

        public int CompareTo(BaseEmployee other)
        {
            return _age - other._age;
        }
    }


}
