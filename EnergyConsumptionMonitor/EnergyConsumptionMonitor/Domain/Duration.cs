using System;
using System.Text.RegularExpressions;

namespace EnergyConsumptionMonitor.Domain
{
    public struct ConsumptionTimeInterval
    {
        public static string pattern = @"(\d*)([smhd])";//TODO: check if this pattern is enough
        public int Value { get; }
        public TimeUnit TimeUnit { get; }
        public bool IsValid { get; set; }

        public ConsumptionTimeInterval(string duration)
        {
            var result = Regex.Match(duration, pattern);
            if (result.Success)
            {
                Value = Convert.ToInt32(result.Groups[1].Value);
                TimeUnit = (TimeUnit)(result.Groups[2].Value.ToUpper()[0]);
                IsValid = true;
            }
            else
            {
                Value = 0;
                TimeUnit = TimeUnit.UNDEFINED;
                IsValid = false;
            }
        }
    }
}
