using OrcHestrador.Domain.Models.Dto;

namespace OrcHestrador.UserStories.Interfaces
{
    public interface IBuscarChamados
    {
        public Task buscarChamados(int group);
    }
}