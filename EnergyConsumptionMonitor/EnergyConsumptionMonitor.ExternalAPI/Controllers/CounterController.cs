using EnergyConsumptionMonitor.ExternalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyConsumptionMonitor.ExternalAPI.Controllers
{
    [Route("api/[controller]")]
    public class CounterController : Controller
    {
        [HttpGet]
        public Counter Get(string id)
        {
            var name = GetVillageName(id);
            return new Counter()
            {
                id = (string.IsNullOrEmpty(name)) ? null : id,
                village_name = name
            };
        }

        public static string GetVillageName(string counter_id)
        {
            if (counter_id == "1")
                return "Villarriba";
            else if (counter_id == "2")
                return "Villabajo";
            else
                return null;
        }
    }
}
