using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }
        public int ITILCategoriesId { get; set; }
        public DateTime? SolveDate { get; set; }

        public int UsersIdRecipient { get; set; }
    }
}

