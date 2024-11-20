using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDTOs
{
    public class EmployeeWithCompanyDetailsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
