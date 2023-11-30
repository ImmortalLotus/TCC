using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;

namespace OrcHestrador.UserStories.Interfaces
{
    public interface ILimparChamados
    {
        public Task PreparaChamado();
    }
}