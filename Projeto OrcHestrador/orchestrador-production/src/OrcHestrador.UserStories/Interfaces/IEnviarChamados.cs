using RestSharp;

namespace OrcHestrador.UserStories.Interfaces
{
    public interface IEnviarChamados
    {
        public Task<RestResponse> EnviarChamado(int IdCategoria, string Json);
    }
}