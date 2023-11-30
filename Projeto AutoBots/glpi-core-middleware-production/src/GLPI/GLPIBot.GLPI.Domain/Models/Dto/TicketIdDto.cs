using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models.Dto
{
    public class TicketIdDto
    {
        [JsonPropertyName("tickets_id")]
        public int TicketId { get; set; }
    }
}
