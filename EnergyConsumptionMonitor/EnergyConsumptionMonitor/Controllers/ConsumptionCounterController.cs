using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Controllers
{
    [Route("api/[controller]")]
    public class ConsumptionCounterController : Controller
    {
        private IConsumptionCounterRepository _repository;
        private IExternalAPIService _externalAPI;

        public ConsumptionCounterController(IConsumptionCounterRepository repository, IExternalAPIService externalAPI)
        {
            _repository = repository;
            _externalAPI = externalAPI;
        }

        [HttpPost("counter_callback")]
        public async Task<ActionResult> counter_callback([FromBody]CounterCallback counter)
        {
            if (counter == null)
                return BadRequest("Please inform counter details");

            var villageName = await _externalAPI.GetVillageNameAsync(counter.Counter_id);
            var consumptionCounter = new ConsumptionCounter(villageName, counter.Counter_id, counter.Amount);
            if (consumptionCounter.IsValid)
            {
                try
                {
                    await _repository.RegisterAsync(consumptionCounter);
                    return Ok(consumptionCounter);
                }
                catch (ConsumptionCounterAlreadyIsRegisteredException)
                {
                    return BadRequest("The counter already is registered.");
                }
            }
            else
                return BadRequest("The counter details supplied are invalid");
        }
    }
}
