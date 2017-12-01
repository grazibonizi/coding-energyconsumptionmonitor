namespace EnergyConsumptionMonitor.ViewModels
{
    public class CounterInfo
    {
        public string Id { get; }
        public string Village_name { get; }

        public CounterInfo(string id, string village_name)
        {
            Id = id;
            Village_name = village_name;
        }
    }
}
