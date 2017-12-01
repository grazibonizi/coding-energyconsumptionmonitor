using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Domain
{
    public interface IConsumptionCounterRepository
    {
        Task<IEnumerable<ConsumptionCounter>> GetByCutOffDateAsync(DateTime cutoffDate);
        Task RegisterAsync(ConsumptionCounter consumptionData);
    }
}
