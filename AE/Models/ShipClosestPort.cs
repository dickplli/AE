using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AE.Models
{
    public class ShipClosestPort
    {
        public Ship Ship { get; set; }
        public ClosestPort CloestPort { get; set; }
        public DateTimeOffset GenerationTime { get; set; }
    }

    public class ClosestPort : Port
    {
        public double Distance { get; set; }
        public DateTimeOffset EstimatedArrivalTime { get; set; }
    }
}
