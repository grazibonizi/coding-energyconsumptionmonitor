using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Infra.Services
{
    public class ExternalAPIService : IExternalAPIService
    {
        private readonly AppSettings _appSettings;

        public ExternalAPIService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<string> GetVillageNameAsync(string counter_id)
        {
            using (var httpClient = new HttpClient())
            {
                var http = string.Format(_appSettings.UrlExternalAPI, counter_id);
                var result = await httpClient.GetStringAsync(http);
                var counterInfo = JsonConvert.DeserializeObject<CounterInfo>(result);
                return counterInfo.Village_name;
            }
        }
    }
}
