using Microsoft.WindowsAzure.Storage.Table;

namespace EnergyConsumptionMonitor.Infra.DataAccess
{
    public class ConsumptionCounterEntity : TableEntity
    {
        public double Amount { get; set; }
        public ConsumptionCounterEntity(string villageName, string counterId)
        {
            PartitionKey = villageName;
            RowKey = counterId;
        }

        public ConsumptionCounterEntity() { }
    }
}
