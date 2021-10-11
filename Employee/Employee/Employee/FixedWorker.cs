namespace Employee
{
    class FixedWorker : BaseEmployee
    {
        public FixedWorker(string Name, int Age, double Payment) : base(Name, Age, Payment)
        {
        }

        public override double Calculate()
        {
            return _payment;
        }
    }


}
