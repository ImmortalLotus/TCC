using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models.Dto
{
    public class GroupTicketDto
    {
        [JsonPropertyName("id")]
        public int GroupTicketId { get; set; }
    }
}
