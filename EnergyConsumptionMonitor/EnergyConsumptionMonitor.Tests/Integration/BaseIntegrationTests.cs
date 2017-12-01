using EnergyConsumptionMonitor.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyConsumptionMonitor.Tests.Integration
{
    public class BaseIntegrationTests
    {
        protected IOptions<AppSettings> Settings { get; }

        public BaseIntegrationTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var mock = new Mock<IOptions<AppSettings>>();
            mock.Setup(ap => ap.Value.SAAccountName).Returns(config.GetValue<string>("App:SAAccountName"));
            mock.Setup(ap => ap.Value.SAKeyValue).Returns(config.GetValue<string>("App:SAKeyValue"));
            mock.Setup(ap => ap.Value.SAKeyName).Returns(config.GetValue<string>("App:SAKeyName"));
            mock.Setup(ap => ap.Value.UrlExternalAPI).Returns(config.GetValue<string>("App:UrlExternalAPI"));
            Settings = mock.Object;
        }
    }
}
