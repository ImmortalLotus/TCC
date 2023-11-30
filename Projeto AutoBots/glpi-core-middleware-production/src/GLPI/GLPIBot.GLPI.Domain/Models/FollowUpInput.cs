using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class FollowUpInput
    {
        public InputF input { get; set; }
        public class InputF
        {
            [JsonPropertyName("tickets_id")]
            [SwaggerSchema("Id do chamado")]
            public string TicketId { get; set; }
            [JsonPropertyName("is_private")]
            [SwaggerSchema("0 - normal, 1 - mensagem privada")]
            public string IsPrivate { get; set; } = "1";
            [JsonPropertyName("requesttypes_id")]
            [SwaggerSchema("Tipo de request no GLPI")]
            public string RequestTypesId { get; set; } = "6";
            [JsonPropertyName("content")]
            [SwaggerSchema("Conteúdo da mensagem")]
            public string Content { get; set; }
        }
    }
}
