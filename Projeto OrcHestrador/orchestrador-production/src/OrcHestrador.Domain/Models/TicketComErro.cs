using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.Domain.Models
{
    public class TicketComErro
    {
        public TicketComErro(int ticketId, string erro)
        {
            TicketId = ticketId;
            Erro = erro;
        }
        public TicketComErro()
        {

        }
        public int TicketId { get; set; }
        public string Erro { get; set; }
    }
}
