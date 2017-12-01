using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Domain
{
    public interface IExternalAPIService
    {
        Task<string> GetVillageNameAsync(string counter_id);
    }
}
