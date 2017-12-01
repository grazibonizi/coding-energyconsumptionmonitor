namespace EnergyConsumptionMonitor.ViewModels
{
    public class CounterCallback
    {
        public string Counter_id { get; }
        public double Amount { get; }

        public CounterCallback(string counter_id, double amount)
        {
            Counter_id = counter_id;
            Amount = amount;
        }
    }
}
