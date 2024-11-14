using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDTOs
{
    public class SignInVisitorDTO
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Company { get; set; }

        public long VisitingCompanyId { get; set; }

        public long AppointmentWithId { get; set; }
    }
}
