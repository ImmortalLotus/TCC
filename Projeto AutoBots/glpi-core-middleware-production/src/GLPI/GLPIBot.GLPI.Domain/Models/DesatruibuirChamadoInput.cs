using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class DesatruibuirChamadoInput
    {
        public InputD input { get; set; }
        public class InputD
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("groups_id")]
            public int GroupId { get; set; }
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("use_notification")]
            public string UseNotification { get; set; }
        }
    }
}
