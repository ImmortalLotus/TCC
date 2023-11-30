using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.UserStories.Interfaces
{
    public interface ITriagemDeChamados
    {
        public Task Avaliar();
    }
}
