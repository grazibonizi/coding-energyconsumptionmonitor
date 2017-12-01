using System.Collections.Generic;

namespace EnergyConsumptionMonitor.ViewModels
{
    public class ConsumptionReport
    {
        public IEnumerable<ConsumptionPerVillage> Villages { get; }

        public ConsumptionReport(IEnumerable<ConsumptionPerVillage> villages)
        {
            Villages = villages;
        }
    }
}
