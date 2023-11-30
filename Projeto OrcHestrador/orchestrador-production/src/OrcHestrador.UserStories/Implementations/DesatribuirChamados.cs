using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;
using Refit;

namespace OrcHestrador.UserStories.Implementations
{
    public class DesatribuirChamados : IDesatribuirChamados
    {
        private readonly IGLPIClient gLPIClient;
        private readonly ILogger<DesatribuirChamados> _logger;
        private readonly OrcHestradorContext _context;
        private readonly GroupOptions groupOptions;

        public DesatribuirChamados(
            IGLPIClient gLPIClient,
            ILogger<DesatribuirChamados> logger,
            OrcHestradorContext context,
            IOptions<GroupOptions> groupOptions
            )
        {
            this.gLPIClient = gLPIClient;
            this._logger = logger;
            _context = context;
            this.groupOptions = groupOptions.Value;
        }

        public async Task desatribuirChamados()
        {
            string avisoCentral = await ErrosImprevistos();
            await ChamadosRecusados(avisoCentral);
            await ChamadosDesaprovados(avisoCentral);
            await ErrosPrevistos(avisoCentral);
            await ChamadosSolucionados();
        }

        private async Task ChamadosSolucionados()
        {
            var chamadosSolucionados = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.Solucionado).ToListAsync();

            foreach (var chamado in chamadosSolucionados)
            {
                try
                {
                    var response2 = await gLPIClient.desatribuirChamado(chamado.TicketId, groupOptions.ValidationGroup);

                    _logger.LogInformation("Chamado : " + chamado.TicketId + " desatribuido");
                    chamado.SituacaoChamado = SituacaoChamado.NoGrupoDeValidacao;
                    _context.tickets.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ocorreu um erro ao desatribuir o chamado: " + chamado.TicketId + " exception:" + ex);

                }
            }
        }

        private async Task ErrosPrevistos(string avisoCentral)
        {
            var chamadosComErroPrevisto = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.ErroPrevisto).ToListAsync();
            foreach (var chamado in chamadosComErroPrevisto)
            {
                var mensagemDeErro = await _context.ticketsComErro.FindAsync(chamado.TicketId);
                FollowUpInputDto followUpInputDto = new FollowUpInputDto();
                followUpInputDto.TicketId = chamado.TicketId.ToString();
                followUpInputDto.IsPrivate = "0";
                followUpInputDto.RequestTypesId = "6";
                followUpInputDto.Content = mensagemDeErro.Erro + avisoCentral;
                try
                {
                    var response = await gLPIClient.addFollowUp(followUpInputDto);
                    var response2 = await gLPIClient.desatribuirChamado(chamado.TicketId, groupOptions.CentralGroup);

                    _logger.LogInformation("Chamado : " + chamado.TicketId + " desatribuido");
                    chamado.SituacaoChamado = SituacaoChamado.Desatribuido;
                    _context.tickets.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (ApiException ex)
                {
                    _logger.LogError("Ocorreu um erro ao desatribuir o chamado: " + chamado.TicketId + " exception:" + ex);
                    await ErroImprevistoDesatribuir(chamado);
                }
            }
        }

        private async Task ChamadosDesaprovados(string avisoCentral)
        {
            var chamadosDesaprovados = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.Desaprovado).ToListAsync();
            foreach (var chamado in chamadosDesaprovados)
            {
                FollowUpInputDto followUpInputDto = new FollowUpInputDto();
                followUpInputDto.TicketId = chamado.TicketId.ToString();
                followUpInputDto.IsPrivate = "0";
                followUpInputDto.RequestTypesId = "6";
                followUpInputDto.Content = "Poxa, que pena, parece que não consegui resolver seu chamado." + avisoCentral;
                try
                {
                    var response = await gLPIClient.addFollowUp(followUpInputDto);
                    var response2 = await gLPIClient.desatribuirChamado(chamado.TicketId, groupOptions.CentralGroup);

                    _logger.LogInformation("Chamado : " + chamado.TicketId + " desatribuido");
                    chamado.SituacaoChamado = SituacaoChamado.Desatribuido;
                    _context.tickets.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (ApiException ex)
                {
                    _logger.LogError("Ocorreu um erro ao desatribuir o chamado: " + chamado.TicketId + " exception:" + ex);
                    await ErroImprevistoDesatribuir(chamado);
                }
            }
        }

        private async Task ChamadosRecusados(string avisoCentral)
        {
            var chamadosRecusados = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.Recusado).ToListAsync();
            foreach (var chamado in chamadosRecusados)
            {
                FollowUpInputDto followUpInputDto = new FollowUpInputDto();
                followUpInputDto.TicketId = chamado.TicketId.ToString();
                followUpInputDto.IsPrivate = "0";
                followUpInputDto.RequestTypesId = "6";
                followUpInputDto.Content = "Seu chamado foi recusado." + avisoCentral;
                try
                {
                    var response = await gLPIClient.addFollowUp(followUpInputDto);
                    var response2 = await gLPIClient.desatribuirChamado(chamado.TicketId, groupOptions.CentralGroup);

                    _logger.LogInformation("Chamado : " + chamado.TicketId + " desatribuido");
                    chamado.SituacaoChamado = SituacaoChamado.Desatribuido;
                    _context.tickets.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (ApiException ex)
                {
                    _logger.LogError("Ocorreu um erro ao desatribuir o chamado: " + chamado.TicketId + " exception:" + ex);
                    await ErroImprevistoDesatribuir(chamado);
                }
            }
        }

        private async Task<string> ErrosImprevistos()
        {
            System.Linq.Expressions.Expression<Func<Domain.Models.Tickets, bool>> condicao = x =>
                                    x.SituacaoChamado == SituacaoChamado.ErroNaLimpeza ||
                                    x.SituacaoChamado == SituacaoChamado.ErroImprevisto ||
                                    x.SituacaoChamado == SituacaoChamado.ErroImprevistoNoSei ||
                                    x.SituacaoChamado == SituacaoChamado.ErroNaBuscaDeUsuario ||
                                    x.SituacaoChamado == SituacaoChamado.UsuarioSemCpf ||
                                    x.SituacaoChamado == SituacaoChamado.ErroImprevistoDesatribuir;
            var chamadosComErroImprevisto =await _context.tickets.Where(condicao).ToListAsync();
            string avisoCentral = "Seu chamado será encaminhado para a central de atendimento.";
            foreach (var chamado in chamadosComErroImprevisto)
            {
                FollowUpInputDto followUpInputDto = new FollowUpInputDto();
                followUpInputDto.TicketId = chamado.TicketId.ToString();
                followUpInputDto.IsPrivate = "0";
                followUpInputDto.RequestTypesId = "6";
                followUpInputDto.Content = "Ocorreu um erro imprevisto ao tentar atender seu chamado." + avisoCentral;
                try
                {
                    if(chamado.SituacaoChamado!=SituacaoChamado.ErroImprevistoDesatribuir)
                    {
                        var response = await gLPIClient.addFollowUp(followUpInputDto);
                    }

                    var response2 = await gLPIClient.desatribuirChamado(chamado.TicketId, groupOptions.CentralGroup);

                    _logger.LogInformation("Chamado : " + chamado.TicketId + " desatribuido");
                    chamado.SituacaoChamado = SituacaoChamado.Desatribuido;
                    _context.tickets.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (ApiException ex)
                {
                    _logger.LogError("Ocorreu um erro ao desatribuir o chamado: " + chamado.TicketId + " exception:" + ex);
                    await ErroImprevistoDesatribuir(chamado);
                }
            }

            return avisoCentral;
        }

        private async Task ErroImprevistoDesatribuir(Tickets chamado)
        {
            chamado.SituacaoChamado = SituacaoChamado.ErroImprevistoDesatribuir;
            _context.tickets.Update(chamado);
            await _context.SaveChangesAsync();
        }
    }
}