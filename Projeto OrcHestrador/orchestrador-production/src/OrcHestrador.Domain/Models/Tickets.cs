using OrcHestrador.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.Domain.Models
{
    public class Tickets
    {


        public Tickets()
        {

        }

        public Tickets(int ticketId, SituacaoChamado situacaoChamado, int categoria, int status, int idUsuario)
        {
            TicketId = ticketId;
            SituacaoChamado = situacaoChamado;
            Categoria = categoria;
            Status = status;
            IdUsuario = idUsuario;
        }

        public int TicketId { get; set; }
        public SituacaoChamado SituacaoChamado { get; set; }
        public int Categoria { get; set; }
        public int Status { get; set; }
        public int IdUsuario { get; set; }
    }
}
