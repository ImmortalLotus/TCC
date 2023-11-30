using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
{
    public class TicketIdDto
    {
        [JsonPropertyName("tickets_id")]
        public int Id { get; set; }
    }
}