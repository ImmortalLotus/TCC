using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLPIBot.GLPI.Domain.Models
{
    public class ITILFollowUp
    {
        public int Id { get; set; }
        public string ItemType { get; set; } = "Ticket";
        public int ItemsId { get; set; }
        public int UsersId { get; set; }
        public int UsersIdEditor { get; set; } = 0;
        public int IsPrivate { get; set; } = 0;
        public int RequestTypesId { get; set; } = 1;
        //Esses últimos 3 campos eu não vi utilidade nenhuma, porém são obrigatórios então tive que mapear.
        public int TimeLinePosition { get; set; } = 4;
        public int SourceItemsId { get; set; } = 0;
        public int SourceOfItemsId { get; set; } = 0;
        public string Content { get; set; }
        //jeito mais simples de contornar o datetime sendo de greenwich.
        public DateTime Date { get; set; } = DateTime.UtcNow.AddHours(-4);
        public DateTime DateMod { get; set; } = DateTime.UtcNow.AddHours(-4);
        public DateTime DateCreation { get; set; } = DateTime.UtcNow.AddHours(-4);
    }
}
