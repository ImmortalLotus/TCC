using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class RulesCriteria
    {
        public int Id { get; set; }
        public int RulesId { get; set; }
        public string Criteria { get; set; }
        public string Pattern { get; set; }
    }
}
