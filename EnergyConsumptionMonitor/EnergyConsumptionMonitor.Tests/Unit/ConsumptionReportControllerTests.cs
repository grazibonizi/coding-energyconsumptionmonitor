using EnergyConsumptionMonitor.Controllers;
using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EnergyConsumptionMonitor.Tests.Unit
{
    public class ConsumptionReportControllerTests
    {
        Mock<IConsumptionCounterRepository> _mockRepo;

        public ConsumptionReportControllerTests()
        {
            _mockRepo = new Mock<IConsumptionCounterRepository>();
        }
       
        [Fact]
        public async Task consumption_report_returns_ok_when_valid_input()
        {
            var duration = "24h";
            _mockRepo.Setup(repo => repo.GetByCutOffDateAsync(It.IsAny<DateTime>()))
                .Returns(Task.FromResult(GetByCutOffDateAsync()));
            var controller = new ConsumptionReportController(_mockRepo.Object);
            var expectedResultObject = new ConsumptionReport(new List<ConsumptionPerVillage>(2) {
                    new ConsumptionPerVillage("Villarriba",10000.123f),
                    new ConsumptionPerVillage("Villabajo",20000.246f)
                });

            var result = await controller.consumption_report(duration);

            var content = ((OkObjectResult)result).Value;
            result.Should().BeOfType<OkObjectResult>();
            content.ShouldBeEquivalentTo(expectedResultObject);
        }
        
        private IEnumerable<ConsumptionCounter> GetByCutOffDateAsync()
        {
            var result = new List<ConsumptionCounter>();
            result.Add(new ConsumptionCounter("Villarriba", "1", 10000.123f));
            result.Add(new ConsumptionCounter("Villabajo", "2", 20000.246f));
            return result;
        }
    }
}
