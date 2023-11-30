using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrcHestrador.ApiClient;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.Infra.Context;
using OrcHestrador.UserStories.Interfaces;
using System.Text;
using System.Text.RegularExpressions;

namespace OrcHestrador.UserStories.Implementations
{
    public class LimparChamados : ILimparChamados
    {
        private readonly OrcHestradorContext _context;
        private readonly ILogger<LimparChamados> _logger;
        private readonly IGLPIClient _gLPIClient;

        public LimparChamados(OrcHestradorContext context, ILogger<LimparChamados> logger, IGLPIClient gLPIClient)
        {
            this._context = context;
            this._logger = logger;
            this._gLPIClient = gLPIClient;
        }

        public async Task PreparaChamado()
        {
            var chamadosALimpar =await _context.tickets.Where(x => x.SituacaoChamado == SituacaoChamado.EsperandoLimpeza).ToListAsync();
            foreach (var ticket in chamadosALimpar)
            {
                string? cpf=String.Empty;
                try
                {
                    var userDto = await _gLPIClient.buscarUsuarioCpf(ticket.TicketId);
                    cpf = userDto.CPF;
                }
                catch (Exception ex)
                {
                    //nao achou usuario / rede off
                    _logger.LogError("Erro ao buscar o usuário do chamado: " + ticket.TicketId + " exception: " + ex.Message);
                    ticket.SituacaoChamado = SituacaoChamado.ErroNaBuscaDeUsuario;
                    continue;
                }
                if (String.IsNullOrEmpty(cpf))
                {
                    //cpf em branco = problema.
                    _logger.LogError("Erro ao buscar o usuário do chamado: " + ticket.TicketId);
                    ticket.SituacaoChamado = SituacaoChamado.UsuarioSemCpf;
                    continue;
                }

                 await TicketTratado(ticket,cpf);

            }
        }

        /// <summary>
        /// Recebe os dados do chamado, limpa eles com regex, valida se eles batem com os campos que aquela API espera receber, 
        /// corrige algumas inconsistências vindas do GLPI e então converte os dados em um dicionário e depois em um 
        /// corpo de requisição POST para uma Api rest JSON
        /// </summary>
        private async Task TicketTratado(Tickets chamado,string cpf)
        {
            try
            {
                var ticket = await _gLPIClient.buscarTicket(chamado.TicketId);
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                string dadosChamadoString = ticket.Content;
                dadosChamadoString = AplicaRegex(dadosChamadoString);
                string[] dadosChamado = SplitString(ref dadosChamadoString);
                var rota = await _context.rotas.Where(x => x.IdCategoria == ticket.IdCategoria).FirstAsync();
                var campos = _context.camposRota.Where(x => x.IdRota == rota.Id);
                foreach (string dado in dadosChamado)
                {
                    string[] campoEValor = dado.Split(":");
                    if (campoEValor.Length > 1)
                    {
                        string campoTratado = campoEValor[0].Replace(" ", "");
                        campoTratado = RemoveAcento(campoEValor[0]);
                        var campoNaApi = await campos.Where(x => x.CampoApi.ToLower().Contains(campoTratado.ToLower()) || x.CampoApi.ToLower().Equals(campoTratado.ToLower()) || campoTratado.ToLower().Contains(x.CampoApi.ToLower())).FirstOrDefaultAsync();
                        if (campoNaApi is not null)
                        {
                            campoTratado = campoNaApi.CampoApi;
                        }
                        dict.Add(campoTratado, RemoveP(campoEValor[1].Trim()));
                    }
                }
                dict.Add("tickedId", ticket.Id);

                if (_context.camposRota.Any(x => x.IdRota == rota.Id && x.CampoApi.Equals("cpf")) && !dict.ContainsKey("cpf"))
                {
                    dict.Add("cpf", cpf);
                }
                var jsonCorreto = JsonConvert.SerializeObject(dict);
                _logger.LogInformation("Chamado " + ticket.Id + ", aberto pelo CPF: " + cpf + ", convertido com sucesso");

                await SalvarLimpeza(chamado, ticket, jsonCorreto);
            }
            catch (Exception ex)
            {
                //finalizou com erro.
                await SalvarErro(chamado);
            }
        }

        private async Task SalvarErro(Tickets chamado)
        {
            chamado.SituacaoChamado = SituacaoChamado.ErroNaLimpeza;
            _context.tickets.Update(chamado);
            await _context.SaveChangesAsync();
        }

        private async Task SalvarLimpeza(Tickets chamado, TicketDto ticket, string jsonCorreto)
        {
            chamado.SituacaoChamado = SituacaoChamado.EsperandoExecucao;
            _context.Update(chamado);
            _context.ticketsRequest.Add(new TicketRequest(ticket.IdCategoria, jsonCorreto, 200, ticket.Id));

            await _context.SaveChangesAsync();
        }

        private static string[] SplitString(ref string dadosChamadoString)
        {
            int index = dadosChamadoString.IndexOf("br");
            dadosChamadoString = dadosChamadoString.Substring(index);
            string[] dadosChamado = dadosChamadoString.Split("br");
            dadosChamado = dadosChamado.Skip(1).ToArray();
            return dadosChamado;
        }

        /// <summary>
        /// Remove o primeiro p de cada string, se ele for um P que não é seguido de vogal ou as letras que ele deveria estar acompanhado
        /// </summary>
        private static string RemoveP(string dado)
        {
            string removePrimeiroP = @"^[p](?![aeiourstláàâãéèêíïóôõöúÁÀÂÃÉÈÍÏÓÔÕÖÚ])";
            dado = Regex.Replace(dado, removePrimeiroP, "");
            return dado;
        }

        private static string AplicaRegex(string dadosChamadoString)
        {
            string regexRemoveSpecialCharacters = @"(&.+?;)";
            string regexRemoveLoneChars = @"\b(?:\d +|[^aeo1234567890])\b\s+";
            string regexRemovePTags = @"(()+(\/p))";

            string regexRemoveEmpty = "nbsp;";
            string regexRemoveDiv = "/div";
            dadosChamadoString = Regex.Replace(dadosChamadoString, regexRemoveSpecialCharacters, "");
            dadosChamadoString = Regex.Replace(dadosChamadoString, regexRemoveLoneChars, "");
            dadosChamadoString = Regex.Replace(dadosChamadoString, regexRemoveEmpty, "");
            dadosChamadoString = Regex.Replace(dadosChamadoString, regexRemovePTags, "");
            dadosChamadoString = Regex.Replace(dadosChamadoString, regexRemoveDiv, "");

            return dadosChamadoString;
        }

        private string RemoveAcento(string texto)
        {
            return
                System.Web.HttpUtility.UrlDecode(
                    System.Web.HttpUtility.UrlEncode(
                        texto, Encoding.GetEncoding("iso-8859-7")));
        }
    }
}