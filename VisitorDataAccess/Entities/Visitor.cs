using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDataAccess.Entities
{
    public class Visitor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Company? Company { get; set; } //string
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
