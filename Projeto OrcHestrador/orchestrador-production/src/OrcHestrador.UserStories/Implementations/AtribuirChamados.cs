using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;

namespace OrcHestrador.UserStories.Implementations
{
    public class AtribuirChamados : IAtribuirChamados
    {
        private readonly IGLPIClient gLPIClient;
        private readonly ILogger<AtribuirChamados> _logger;
        private readonly MensagemOptions _mOptions;
        private readonly OrcHestradorContext _context;

        public AtribuirChamados(IGLPIClient gLPIClient, ILogger<AtribuirChamados> logger, IOptions<MensagemOptions> mOptions, OrcHestradorContext context)
        {
            this.gLPIClient = gLPIClient;
            this._logger = logger;
            this._mOptions = mOptions.Value;
            _context = context;
        }

        public async Task atribuirChamados()
        {
            var tickets= _context.tickets.Where(x=>x.SituacaoChamado == SituacaoChamado.Buscado).ToList();
            foreach (var ticket in tickets)
            {
                var followUps = await gLPIClient.buscarFollowUpPorTicket(ticket.TicketId);
                if (followUps.Count > 0 && followUps.Any(x => x.UsersId == 15712))
                {
                    _logger.LogInformation("Chamado : " + ticket.TicketId + " já foi atribuido anteriormente");
                    AtualizaStatusTicket(ticket);
                    continue;
                }

                var atribuirResult = await gLPIClient.atribuirChamado(ticket.TicketId);
                if (!atribuirResult.IsSuccessStatusCode)
                {
                    _logger.LogError("Ocorreu um erro ao atribuir o chamado: " + ticket.TicketId);
                    continue;
                }
                //manda um oi
                await MensagemDeOla(ticket);
                //Att status no banco.
                AtualizaStatusTicket(ticket);
            }
        }

        private void AtualizaStatusTicket(Tickets ticket)
        {
            ticket.SituacaoChamado = SituacaoChamado.Atribuido;
            _context.tickets.Update(ticket);
            _context.SaveChanges();
        }

        private async Task MensagemDeOla(Tickets ticket)
        {
            FollowUpInputDto followUpInput = new FollowUpInputDto();
            followUpInput.TicketId = ticket.TicketId.ToString();
            followUpInput.IsPrivate = "0";
            followUpInput.RequestTypesId = "6";
            Random random = new Random();
            followUpInput.Content = _mOptions.Hellos[random.Next(0, 10)];
            var addFollowUpResult = await gLPIClient.addFollowUp(followUpInput);
            _logger.LogInformation("Chamado : " + ticket.TicketId + " atribuido");
        }
    }
}