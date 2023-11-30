using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
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
        public int IdCategoria { get; set; }
        [JsonPropertyName("users_id_recipient")]
        public int idUsuarioRequerente { get; set; }
    }
}