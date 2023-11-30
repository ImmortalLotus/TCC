using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;

namespace OrcHestrador.UserStories.Implementations
{
    public class BuscarChamados : IBuscarChamados
    {
        private readonly IGLPIClient gLPIClient;
        private readonly ILogger<BuscarChamados> _logger;
        private readonly OrcHestradorContext _context;

        public BuscarChamados(IGLPIClient gLPIClient, ILogger<BuscarChamados> logger, OrcHestradorContext context)
        {
            this.gLPIClient = gLPIClient;
            this._logger = logger;
            _context = context;
        }

        public async Task buscarChamados(int group)
        {
            try
            {
                var chamados = await gLPIClient.buscarTicketsPorGrupo(group);

                List<TicketDto> tickets = new List<TicketDto>();
                foreach (var chamado in chamados)
                {
                    tickets.Add(await gLPIClient.buscarTicket(chamado.Id));
                }
                //merge insert
                    var ticketIds = tickets.Select(x => x.Id);
                    var ticketsPraExcluir = await _context.tickets.Where(x=>ticketIds.Contains(x.TicketId)).Select(x=>x.TicketId).ToListAsync();
                    var ticketsFiltrados = tickets.Where(x => !ticketsPraExcluir.Contains(x.Id)).ToList();
                //
                ticketsFiltrados.ForEach(x => _context.tickets.Add(new Tickets(x.Id, SituacaoChamado.Buscado, x.IdCategoria, x.Status, x.idUsuarioRequerente)));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao buscar chamados " + ex.Message);
            }
        }
    }
}