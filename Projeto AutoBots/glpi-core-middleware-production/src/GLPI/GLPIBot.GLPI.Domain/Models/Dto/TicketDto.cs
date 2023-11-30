using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models.Dto
{
    public class TicketDto
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("itilcategories_id")]
        public int Category { get; set; }
        [JsonPropertyName("users_id_recipient")]
        public int UsuarioCliente { get; set; }
    }
}
