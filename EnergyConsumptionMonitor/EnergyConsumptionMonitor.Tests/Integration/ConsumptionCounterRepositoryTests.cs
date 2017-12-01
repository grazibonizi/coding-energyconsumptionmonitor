using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.Infra.DataAccess;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EnergyConsumptionMonitor.Tests.Integration
{
    public class ConsumptionCounterRepositoryTests : BaseIntegrationTests
    {
        private readonly ConsumptionCounterRepository _repository;
        public ConsumptionCounterRepositoryTests()
        {
            _repository = new ConsumptionCounterRepository(base.Settings);
        }

        [Fact]
        public async Task CloudRepositoryIsValidAsync()
        {
            var villageName = Guid.NewGuid().ToString();
            var consumptionCounter = new ConsumptionCounter(villageName, Guid.NewGuid().ToString(), 1000.123);
            var cutOffDate = DateTime.Now.AddDays(-24);
            bool isValid = false;
            await Task.Run(async () => {
                await _repository.RegisterAsync(consumptionCounter);
                await _repository.GetByCutOffDateAsync(cutOffDate);
                try
                {
                    await _repository.RegisterAsync(consumptionCounter);
                }
                catch (ConsumptionCounterAlreadyIsRegisteredException)
                {
                    isValid = true;
                }
            });

            isValid.Should().BeTrue();
        }
    }
}
