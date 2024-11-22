using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VisitorDTOs
{
    public class VisitorRegistrationSearchApiResponse
    {
        [JsonPropertyName("$values")]
        public List<VisitorRegistrationSearchDTO> Values { get; set; }
    }
}
