using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
	public struct Result
    {
        public int Id { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
