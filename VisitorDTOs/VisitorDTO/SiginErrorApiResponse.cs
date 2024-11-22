using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VisitorDTOs.VisitorDTO
{
    public class SiginErrorApiResponse
    {
        [JsonPropertyName("errors")]
        public string errors { get; set; }
    }
}
