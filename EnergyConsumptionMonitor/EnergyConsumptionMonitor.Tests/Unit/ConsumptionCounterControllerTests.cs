using EnergyConsumptionMonitor.Controllers;
using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EnergyConsumptionMonitor.Tests.Unit
{
    public class ConsumptionCounterControllerTests
    {
        Mock<IConsumptionCounterRepository> _mockRepo;
        Mock<IExternalAPIService> _mockExternalAPI;

        public ConsumptionCounterControllerTests()
        {
            _mockRepo = new Mock<IConsumptionCounterRepository>();
            _mockExternalAPI = new Mock<IExternalAPIService>();
        }
       
        [Fact]
        public async Task counter_callback_returns_ok_when_valid_input()
        {
            var counter = new CounterCallback("1",10000.123);
            _mockExternalAPI.Setup(api => api.GetVillageNameAsync(counter.Counter_id))
                .Returns(Task.FromResult(GetVillageName(counter.Counter_id)));
            var expectedResultObject = new ConsumptionCounter(GetVillageName(counter.Counter_id), counter.Counter_id, counter.Amount);

            _mockRepo.Setup(repo => repo.RegisterAsync(expectedResultObject)).Returns(RegisterAsync());
            var controller = new ConsumptionCounterController(_mockRepo.Object, _mockExternalAPI.Object);

            var result = await controller.counter_callback(counter);

            var content = ((OkObjectResult)result).Value;
            result.Should().BeOfType<OkObjectResult>();
            content.ShouldBeEquivalentTo(expectedResultObject);
        }

        public async Task RegisterAsync()
        {
            await Task.Delay(1);
        }

        private string GetVillageName(string counter_id)
        {
            switch (counter_id)
            {
                default:
                    return null;
                case "1":
                    return "Villarriba";
                case "2":
                    return "Villabajo";
            }
        }
    }
}
