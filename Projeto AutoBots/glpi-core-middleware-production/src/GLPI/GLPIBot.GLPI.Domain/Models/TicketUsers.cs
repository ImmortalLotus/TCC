using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class TicketUsers
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int TicketId { get; set; }

        //o tipo é referente ao tipo de atribuição(requerente observador e atribuido)
        public int Type { get; set; }
    }
}
