using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class Group_Ticket
    {
        public int Id { get; set; }
        public int Tickets_id { get; set; }
        public int Groups_id { get; set; }
        public int Type { get; set; }
    }
}
