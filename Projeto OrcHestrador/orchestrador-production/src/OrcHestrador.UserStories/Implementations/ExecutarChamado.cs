using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;
using RestSharp;
using System.Net.Sockets;

namespace OrcHestrador.UserStories.Implementations
{
    public class ExecutarChamado : IExecutarChamado
    {
        private readonly IEnviarChamados _enviarChamados;
        private readonly ILogger<ExecutarChamado> _logger;

        private readonly MensagemOptions mOptions;

        private readonly OrcHestradorContext _context;

        public ExecutarChamado(
            IEnviarChamados enviarChamados,
            ILogger<ExecutarChamado> logger,
            IOptions<MensagemOptions> mOptions, 
            OrcHestradorContext context)
        {
            this._enviarChamados = enviarChamados;
            this._logger = logger;
            this.mOptions = mOptions.Value;
            _context = context;
        }


        public async Task Executar()
        {
            var requests = await _context.ticketsRequest.ToListAsync();
            foreach (var request in requests)
            {
                RestResponse response = new RestResponse();
                Random random = new Random();


                response = await _enviarChamados.EnviarChamado(request.IdCategoria, request.Json);
                _logger.LogInformation("Chamado : " + request.TicketId + " enviado");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Automação retornou erro");
                    await TratarErrorResponse(request, response);
                }
                else
                {
                    var ticketQueDeuCerto = await _context.tickets.FindAsync(request.TicketId);
                    ticketQueDeuCerto.SituacaoChamado = SituacaoChamado.EsperandoSolucao;
                    _context.tickets.Update(ticketQueDeuCerto);
                    await _context.SaveChangesAsync();
                }
                await _context.ticketsRequest.Where(x => x.TicketId == request.TicketId).ExecuteDeleteAsync();
            }
        }

       

        private async Task TratarErrorResponse(TicketRequest request, RestResponse response)
        {
            try
            {
                var dictio = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Content);

                Random random = new Random();

                var ticket = await _context.tickets.FindAsync(request.TicketId);
                if (!dictio.ContainsKey("message"))
                {
                    _logger.LogError("O erro encontrado no sei não estava previsto");

                    ticket.SituacaoChamado = SituacaoChamado.ErroImprevistoNoSei;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    string erro = dictio["message"];
                    string msg = mOptions.Errors[random.Next(0, 12)] + erro;
                    _logger.LogError("Erro encontrado no sei: " + erro);
                    if ((int)response.StatusCode == (int)CodigosDeStatus.SemPermissao)
                    {
                        msg = erro;
                    }
                    ticket.SituacaoChamado = SituacaoChamado.ErroPrevisto;
                    _context.Update(ticket);
                    _context.ticketsComErro.Add(new TicketComErro(request.TicketId, msg));
                    await _context.SaveChangesAsync();
                }
            }catch (Exception)
            {
                var ticket = await _context.tickets.FindAsync(request.TicketId);
                ticket.SituacaoChamado = SituacaoChamado.ErroImprevisto;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
