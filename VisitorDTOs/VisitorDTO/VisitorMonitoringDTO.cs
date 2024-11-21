using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDTOs.VisitorDTO
{
    public class VisitorMonitoringDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string VisitingComapanyName { get; set; }
        public string AppointmentWith { get; set; }
        public DateTime StartTime { get; set; }
    }
}
