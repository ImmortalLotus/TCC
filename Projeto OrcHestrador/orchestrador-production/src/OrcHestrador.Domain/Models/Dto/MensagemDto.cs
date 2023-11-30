using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
{
    public class MensagemDto
    {
        [JsonPropertyName("mensagem")]
        [SwaggerSchema("A mensagem que será enviada ao GLPI")]
        public string Message { get; set; }

        [JsonPropertyName("url_origem")]
        [SwaggerSchema("A url de origem da mensagem que será enviada ao GLPI")]
        public string UrlOrigem { get; set; }

        [JsonPropertyName("status")]
        [SwaggerSchema("O status da mensagem que será enviada ao GLPI")]
        public bool Status { get; set; }

        [JsonPropertyName("ticket_id")]
        [SwaggerSchema("O id do chamado que vai receber a mensagem no GLPI")]
        public int TicketId { get; set; }
    }
}