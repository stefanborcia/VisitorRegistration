using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VisitorDTOs.VisitorDTO;

namespace VisitorDTOs
{
    public class VisitorMonitoringApiResponse
    {
        [JsonPropertyName("$values")]
        public List<VisitorMonitoringDTO> Values { get; set; }
    }
}
