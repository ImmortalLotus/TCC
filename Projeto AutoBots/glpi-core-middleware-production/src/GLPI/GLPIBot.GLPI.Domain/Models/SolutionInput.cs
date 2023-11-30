using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class SolutionInput
    {
        public InputS input { get; set; }
        public class InputS
        {

            [JsonPropertyName("items_id")]
            [SwaggerSchema("Id do chamado")]
            public string TicketId { get; set; }

            [JsonPropertyName("solutiontypes_id")]
            [SwaggerSchema("Tipo de solução do GLPI")]
            public string SolutionType { get; set; } = "2";

            [JsonPropertyName("itemtype")]
            [SwaggerSchema("Tipo do item, ex: Ticket, FollowUp...")]
            public string ItemType { get; set; } = "Ticket";

            [JsonPropertyName("content")]
            [SwaggerSchema("Conteúdo da Solução")]
            public string Content { get; set; }
        }
    }
}
