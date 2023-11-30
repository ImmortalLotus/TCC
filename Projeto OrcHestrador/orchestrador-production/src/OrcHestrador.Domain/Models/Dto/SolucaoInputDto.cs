using System.Text.Json.Serialization;

namespace OrcHestrador.Domain.Models.Dto
{
    public class SolucaoInputDto
    {
        [JsonPropertyName("items_id")]
        public string TicketId { get; set; }

        [JsonPropertyName("solutiontypes_id")]
        public string SolutionType { get; set; }

        [JsonPropertyName("itemtype")]
        public string ItemType { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}