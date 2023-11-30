using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class TicketValidationDto
    {
        [JsonPropertyName("tickets_id")]
        public int TicketId { get; set; }
        [JsonPropertyName("users_id_validate")]
        public int UsuarioValidador { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }

    }
}
