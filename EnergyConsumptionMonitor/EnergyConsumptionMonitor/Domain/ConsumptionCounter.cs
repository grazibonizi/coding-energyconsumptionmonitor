namespace EnergyConsumptionMonitor.Domain
{
    public struct ConsumptionCounter
    {
        public string VillageName { get; }
        public string CounterId { get; }
        public double Amount { get; }
        public bool IsValid { get; }

        public ConsumptionCounter(string villageName,
            string counterId,
            double Amount)
        {
            VillageName = villageName;
            this.Amount = Amount;
            CounterId = counterId;
            IsValid = !string.IsNullOrWhiteSpace(VillageName) &&
                !string.IsNullOrWhiteSpace(CounterId) &&
                this.Amount >= 0;
        }
    }
}
