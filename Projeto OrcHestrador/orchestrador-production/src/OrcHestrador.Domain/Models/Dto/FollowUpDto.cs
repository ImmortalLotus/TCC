using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
{
    public class FollowUpDto
    {
        [JsonPropertyName("items_id")]
        public int TicketId { get; set; }

        [JsonPropertyName("users_id")]
        public int UsersId { get; set; }
    }
}