using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Controllers
{
    [Route("api/[controller]")]
    public class ConsumptionReportController : Controller
    {
        private IConsumptionCounterRepository _repository;

        public ConsumptionReportController(IConsumptionCounterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("consumption_report")]
        public async Task<ActionResult> consumption_report(string duration)
        {
            var timeInterval = new ConsumptionTimeInterval(duration);
            if (!timeInterval.IsValid)
                return BadRequest("The time interval supplied is invalid");

            var cutOffDate = GetCutOffDateByTimeInterval(DateTime.Now, timeInterval);
            var consumptionCounterFilteredByDate = await _repository.GetByCutOffDateAsync(cutOffDate);

            var consumptionPerVillage = consumptionCounterFilteredByDate
                .GroupBy(a => a.VillageName)
                .Select(x => new ConsumptionPerVillage(x.Key, x.Sum(y => y.Amount)));

            var result = new ConsumptionReport(consumptionPerVillage);

            return Ok(result);
        }

        public DateTime GetCutOffDateByTimeInterval(DateTime now, ConsumptionTimeInterval duration)
        {
            switch (duration.TimeUnit)
            {
                case TimeUnit.SECOND:
                    return now.AddSeconds((-1) * duration.Value);
                case TimeUnit.MINUTE:
                    return now.AddMinutes((-1) * duration.Value);
                case TimeUnit.HOUR:
                    return now.AddHours((-1) * duration.Value);
                case TimeUnit.DAY:
                    return now.AddDays((-1) * duration.Value);
                default:
                    return DateTime.MinValue;
            }
        }
    }
}
