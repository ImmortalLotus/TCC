using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.UserStories.Implementations
{
    public class TriagemDeChamados : ITriagemDeChamados
    {
        private readonly IGLPIClient _gLPIClient;
        private readonly OrcHestradorContext _context;
        private readonly ILogger<TriagemDeChamados> logger;

        public TriagemDeChamados(
            IGLPIClient gLPIClient,
            OrcHestradorContext context, ILogger<TriagemDeChamados> logger)
        {
            this._gLPIClient = gLPIClient;
            this._context = context;
            this.logger = logger;
        }
        public async Task Avaliar()
        {
            var chamadosAAvaliar = await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.Atribuido || x.SituacaoChamado==SituacaoChamado.EsperandoValidacao).ToListAsync();
            foreach (var ticket in chamadosAAvaliar)
            {

                var categoria = await _context.categorias.Where(x=>x.Id==ticket.Categoria).FirstAsync();
                if (categoria is null)
                {
                    ticket.SituacaoChamado = SituacaoChamado.CategoriaInvalida;
                }
                else if (categoria.tipoValidacao == TipoValidacao.ValidacaoComeco)
                {
                    var validacoesTicket = await _gLPIClient.buscarValidacoesTicket(ticket.TicketId);
                    if (validacoesTicket.Any(x => x.UsuarioValidador == ticket.IdUsuario))
                    {
                        //o usuário faz parte do grupo validador.
                        ticket.SituacaoChamado = SituacaoChamado.EsperandoLimpeza;
                    }
                    else if (!validacoesTicket.Any(x => x.UsuarioValidador == ticket.IdUsuario) && validacoesTicket.All(x => x.Status == 2))
                    {
                        //chamado ainda não foi aprovado
                        ticket.SituacaoChamado = SituacaoChamado.EsperandoValidacao;
                    }
                    else if (!validacoesTicket.Any(x => x.UsuarioValidador == ticket.IdUsuario) && validacoesTicket.Any(x => x.Status == 4))
                    {
                        //chamado foi recusado.
                        ticket.SituacaoChamado = SituacaoChamado.Recusado;
                    }
                    else if (!validacoesTicket.Any(x => x.UsuarioValidador == ticket.IdUsuario) && validacoesTicket.Any(x => x.Status == 3))
                    {
                        //chamado foi aprovado.
                        ticket.SituacaoChamado = SituacaoChamado.EsperandoLimpeza;
                    }
                    else
                    {
                        //status desconhecido
                        ticket.SituacaoChamado = SituacaoChamado.ErroImprevisto;
                    }
                }
                else
                {
                    //não é um chamado com validação
                    ticket.SituacaoChamado = SituacaoChamado.EsperandoLimpeza;
                }

                //atualizar status do chamado.
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
