using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class TicketValidations
    {
        public int Id { get; set; }
        public int EntitiesId { get; set; }
        public int UsersId { get; set; }
        public int TicketsId { get; set; }
        public int UsersIdValidate { get; set; }
        public int Status { get; set; }
    }
}
