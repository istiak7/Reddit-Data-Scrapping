using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Common.Utilities
{
    public static class CommonMethods
    {
        public static int Interval {  get; private set; }
        public static void Initialize(IConfiguration config)
        {
            Interval = Convert.ToInt32(config["TimeSettings:TimeZoneOffset"]);
        }
        public static DateTime GetBDCurrentTime()
        {
            return DateTime.UtcNow.AddHours(Interval);
        }
    }
}
