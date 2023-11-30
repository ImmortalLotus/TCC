using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{

    public class AtribuirInput
    {
        public AtribuirInput(string idDoUsuario)
        {
            this.input = new InputA(idDoUsuario);
        }
        [JsonPropertyName("input")]
        public InputA input { get; set; }
        public class InputA
        {

            public InputA(string usuarioAssign)
            {
                this.usuarioAssign = usuarioAssign;
            }

            [JsonPropertyName("_users_id_assign")]
            public string usuarioAssign { get; set; }
        }
    }
}
