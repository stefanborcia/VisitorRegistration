using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VisitorDTOs
{
    public class EmployeeApiResponse
    {
        [JsonPropertyName("$values")]
        public List<EmployeeDTO>? Values { get; set; }
    }
}
