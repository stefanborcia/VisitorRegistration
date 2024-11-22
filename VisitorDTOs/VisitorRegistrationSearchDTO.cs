using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDTOs
{
    public class VisitorRegistrationSearchDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string VisitingComapanyName { get; set; }
        public string AppointmentWithEmployeeName { get; set; }
        public string TimeSpent { get; set; }
        public DateTime StartTime { get; set; }
        public string Date => StartTime.ToString("yyyy-MM-dd");
    }
}
