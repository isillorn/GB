namespace Employee
{
    class HourlyWorker : BaseEmployee
    {
        public HourlyWorker(string Name, int Age, double Payment) : base(Name, Age, Payment)
        {
        }

        public override double Calculate()
        {
            return 20.8 * 8 * _payment;
        }
    
    
    
    }


}
