using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.UserStories.Implementations
{
    public class SolucionarChamado : ISolucionarChamado
    {
        private readonly IGLPIClient _gLPIClient;
        private readonly MensagemOptions mOptions;
        private readonly OrcHestradorContext _context;
        private readonly ILogger<SolucionarChamado> _logger;
        private readonly GroupOptions groupOptions;

        public SolucionarChamado(IGLPIClient gLPIClient,
             IOptions<MensagemOptions> mOptions,
             OrcHestradorContext context,
             ILogger<SolucionarChamado> logger,
             IOptions<GroupOptions> groupOptions)
        {
            this._gLPIClient = gLPIClient;
            this.mOptions = mOptions.Value;
            _context = context;
            this._logger = logger;
            this.groupOptions = groupOptions.Value;
        }
        public async Task Executar()
        {
            var ticketsASolucionar = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.EsperandoSolucao || x.SituacaoChamado == SituacaoChamado.ErroNaSolucao).ToListAsync();

            foreach (var request in ticketsASolucionar)
            {
                Random random = new Random();
                SolucaoInputDto input = new SolucaoInputDto();
                input.SolutionType = "2";
                input.TicketId = request.TicketId.ToString();
                input.ItemType = "Ticket";
                input.Content = mOptions.Oks[random.Next(0, 9)];
                var ticket = await _context.tickets.FindAsync(request.TicketId);
                var solucaoResponse = await _gLPIClient.addSolucao(input);
                if (!solucaoResponse.IsSuccessStatusCode)
                {
                    ticket.SituacaoChamado = SituacaoChamado.ErroNaSolucao;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    _logger.LogError("Erro ao solucionar o chamado: " + request.TicketId);
                    return;
                }
                var resp = await _gLPIClient.desatribuirChamado(request.TicketId, groupOptions.ValidationGroup);
                ticket.SituacaoChamado = SituacaoChamado.Solucionado;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Chamado " + request.TicketId + " solucionado com sucesso");
            }
        }
    }
}
