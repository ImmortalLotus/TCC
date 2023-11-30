using System.Text.Json.Serialization;

namespace GLPIBot.GLPI.Domain.Models.Dto
{
    public class SessionDto
    {
        [JsonPropertyName("session_token")]
        public string SessionToken { get; set; }
    }
}
