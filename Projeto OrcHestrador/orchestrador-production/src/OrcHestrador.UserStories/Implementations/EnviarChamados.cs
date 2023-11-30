using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Helpers;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;
using RestSharp;

namespace OrcHestrador.UserStories.Implementations
{
    public class EnviarChamados : IEnviarChamados
    {
        private readonly OrcHestradorContext _context;
        private readonly ILogger<EnviarChamados> _logger;
        private readonly RequestOptions _reqOptions;

        public EnviarChamados(OrcHestradorContext context, ILogger<EnviarChamados> logger, IOptions<RequestOptions> reqOptions)
        {
            this._context = context;
            this._logger = logger;
            this._reqOptions = reqOptions.Value;
        }

        public async Task<RestResponse> EnviarChamado(int IdCategoria, string Json)
        {
            var rota = await _context.rotas.Where(x => x.IdCategoria == IdCategoria).FirstAsync();
            var automacao = await _context.automacoes.FindAsync(rota.IdAutomacao);
            var client = new RestClient(new RestClientOptions(automacao.Url));
            var request = new RestRequest(rota.RotaApi, Method.Post);
            request.AddHeader("Authorization", CaesarDecrypter.Decrypt(automacao.SenhaAutomacao));
            request.AddHeader("Origin", _reqOptions.Url);
            request.AddHeader("Content-Type", "application/json");
            var body = Json;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}