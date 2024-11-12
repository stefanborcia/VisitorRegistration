using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDataAccess.Entities
{
    public class Visit : SoftDelete
    {
        public long Id { get; set; }
        public Visitor Visitor {  get; set; }
        public Company VisitingCompany { get; set; }
        public Employee AppointmentWith { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
        public Action CurrentStatus { get; set; } = Action.SignOut;
    }
}
