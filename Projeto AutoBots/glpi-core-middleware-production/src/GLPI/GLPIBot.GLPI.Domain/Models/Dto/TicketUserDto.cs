using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models.Dto
{
    public class TicketUserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("users_id")]
        public int userId { get; set; } 
    }
}
