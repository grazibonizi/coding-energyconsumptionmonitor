using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyConsumptionMonitor.Domain
{
    public class AppSettings
    {
        public virtual string UrlExternalAPI { get; set; }
        public virtual string SAAccountName {get;set;}
        public virtual string SAKeyValue { get; set; }
        public virtual string SAKeyName { get; set; }
    }
}
