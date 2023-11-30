using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;

namespace OrcHestrador.UserStories.Implementations
{
    public class ValidarChamados : IValidarChamados
    {
        private readonly IBuscarChamados _buscarChamados;
        private readonly IDesatribuirChamados _desatribuirChamados;
        private readonly ILogger<ValidarChamados> _logger;
        private readonly MensagemOptions mOptions;
        private readonly GroupOptions _glpiOptions;
        private readonly OrcHestradorContext _context;
        private readonly IGLPIClient _gLPIClient;

        public ValidarChamados(
            IBuscarChamados buscarChamados,
            IOptions<GroupOptions> glpiOptions,
            IDesatribuirChamados desatribuirChamados,
            ILogger<ValidarChamados> logger,
            IOptions<MensagemOptions> mOptions,
            OrcHestradorContext context, 
            IGLPIClient gLPIClient
            )
        {
            this._buscarChamados = buscarChamados;
            this._desatribuirChamados = desatribuirChamados;
            this._logger = logger;
            this.mOptions = mOptions.Value;
            this._glpiOptions = glpiOptions.Value;
            _context = context;
            this._gLPIClient = gLPIClient;
        }

        public async Task ValidaChamados()
        {
            var chamados = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.NoGrupoDeValidacao).ToListAsync();
            foreach (var chamado in chamados)
            {
                var ticket = await _gLPIClient.buscarTicket(chamado.TicketId);
                if (ticket.Status == 2)
                {
                    _logger.LogInformation("Encontrado chamado desaprovado: " + ticket.Id);
                    chamado.SituacaoChamado = SituacaoChamado.Desaprovado;
                    _context.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                else if(ticket.Status==6)
                {
                    chamado.SituacaoChamado = SituacaoChamado.Finalizado;
                    _context.Update(chamado);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}