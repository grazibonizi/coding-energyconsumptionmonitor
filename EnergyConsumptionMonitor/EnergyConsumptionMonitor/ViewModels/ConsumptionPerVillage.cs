namespace EnergyConsumptionMonitor.ViewModels
{
    public class ConsumptionPerVillage
    {
        public string Village_name { get; }
        public double Consumption { get; }

        public ConsumptionPerVillage(string village_name, double consumption)
        {
            Village_name = village_name;
            Consumption = consumption;
        }
    }
}
