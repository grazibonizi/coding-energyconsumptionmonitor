using EnergyConsumptionMonitor.Domain;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Infra.DataAccess
{
    public class ConsumptionCounterRepository : IConsumptionCounterRepository
    {
        private const string consumptionCounterTableName = "ConsumptionCounter";
        private readonly CloudTable _consumptionCounterTable;
        private readonly AppSettings _appSettings;

        public ConsumptionCounterRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            var credentials = new StorageCredentials(_appSettings.SAAccountName, _appSettings.SAKeyValue, _appSettings.SAKeyName);
            CloudStorageAccount storageAccount = new CloudStorageAccount(credentials, true);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            _consumptionCounterTable = tableClient.GetTableReference(consumptionCounterTableName);
        }

        public async Task<IEnumerable<ConsumptionCounter>> GetByCutOffDateAsync(DateTime cutoffDate)
        {
            var queryFilter = $"Timestamp ge datetime'{cutoffDate.ToUniversalTime().ToString("s")}Z'";
            return await FilterAsync(queryFilter);
        }

        public async Task RegisterAsync(ConsumptionCounter consumptionCounter)
        {
            var exists = await ChecIfEntityExists(consumptionCounter.VillageName, consumptionCounter.CounterId);
            if (exists)
                throw new ConsumptionCounterAlreadyIsRegisteredException();

            var entity = new ConsumptionCounterEntity(consumptionCounter.VillageName, consumptionCounter.CounterId);
            entity.Amount = consumptionCounter.Amount;
            var insertEntityOperation = TableOperation.Insert(entity);
            await _consumptionCounterTable.ExecuteAsync(insertEntityOperation);
        }

        public async Task<bool> ChecIfEntityExists(string partitionKey, string rowKey)
        {
            var checkIfIsRegisteredQuery = $"PartitionKey eq '{partitionKey}' and RowKey eq '{rowKey}'";
            return (await FilterAsync(checkIfIsRegisteredQuery)).Any();
        }

        public async Task<IEnumerable<ConsumptionCounter>> FilterAsync(string queryFilter)
        {
            var result = new List<ConsumptionCounter>();
            var query = new TableQuery<ConsumptionCounterEntity>().Where(queryFilter);
            TableContinuationToken token = null;
            do
            {
                var resultSegment = await _consumptionCounterTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (var entity in resultSegment.Results)
                {
                    result.Add(new ConsumptionCounter(entity.PartitionKey,
                        entity.RowKey,
                        entity.Amount
                    ));
                }
            } while (token != null);
            return result;
        }
    }
}
