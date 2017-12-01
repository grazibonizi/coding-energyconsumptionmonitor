using EnergyConsumptionMonitor.Infra.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EnergyConsumptionMonitor.Tests.Integration
{
    public class ExternalAPIServiceTests : BaseIntegrationTests
    {
        private readonly ExternalAPIService _externalAPIService;
        public ExternalAPIServiceTests()
        {
            _externalAPIService = new ExternalAPIService(base.Settings);
        }

        [Fact]
        public async Task ExternalAPIServiceIsValid()
        {
            var counterId = Guid.NewGuid().ToString();
            var villageName = await _externalAPIService.GetVillageNameAsync(counterId);
            Assert.Null(villageName);
        }
    }
}
