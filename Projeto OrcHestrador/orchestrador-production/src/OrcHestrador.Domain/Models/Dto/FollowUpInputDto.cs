using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
{
    public class FollowUpInputDto
    {
        [JsonPropertyName("tickets_id")]
        public string TicketId { get; set; }

        [JsonPropertyName("is_private")]
        public string IsPrivate { get; set; }

        [JsonPropertyName("requesttypes_id")]
        public string RequestTypesId { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}