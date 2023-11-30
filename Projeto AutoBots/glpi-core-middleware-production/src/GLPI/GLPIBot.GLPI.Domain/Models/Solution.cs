using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class Solution
    {
        public int Id { get; set; }
        public string ItemType { get; set; } = "Ticket";
        public int ItemsId { get; set; }
        public int SolutionTypesId { get; set; }= 0;
        public string Content { get; set; }
        public int UsersId { get; set; }
        public DateTime DateMod { get; set; } = DateTime.UtcNow.AddHours(-4);
        public DateTime DateCreation { get; set; } = DateTime.UtcNow.AddHours(-4);
        public int Status { get; set; } = 2;
    }
}
