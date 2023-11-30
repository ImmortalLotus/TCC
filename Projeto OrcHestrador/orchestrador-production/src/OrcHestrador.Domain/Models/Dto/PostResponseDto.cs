using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
{
    public struct PostResponseDto
    {
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}