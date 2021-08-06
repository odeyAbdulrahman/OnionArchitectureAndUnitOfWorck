using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Dtos
{
    public class HealthCheckResponseModel
    {
        public string Status { get; set; }
        public IEnumerable<HealthCheckModel> Checks { get; set; }
        public TimeSpan Duration { get; set; }
    }
    public class HealthCheckModel
    {
        public string Status { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
    }
}
