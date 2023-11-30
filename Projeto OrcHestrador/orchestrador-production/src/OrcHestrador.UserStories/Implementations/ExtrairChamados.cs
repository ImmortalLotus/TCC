using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Enums;
using OrcHestrador.UserStories.Interfaces;
using OrcHestrador.UserStories.Options;

namespace OrcHestrador.UserStories.Implementations
{
    public class AvaliarChamados : IAvaliarChamados
    {
        private readonly IBuscarChamados _buscarChamados;
        private readonly ILimparChamados _limparChamados;
        private readonly IAtribuirChamados _atribuirChamados;
        private readonly ITriagemDeChamados _triagemDeChamados;
        private readonly GroupOptions groupOptions;

        public AvaliarChamados(
            IBuscarChamados buscarChamados,
            ILimparChamados limparChamados,
            IAtribuirChamados atribuirChamados,
            IOptions<GroupOptions> groupOptions,
            ITriagemDeChamados _triagemDeChamados)
        {
            _buscarChamados = buscarChamados;
            _limparChamados = limparChamados;
            _atribuirChamados = atribuirChamados;
            this._triagemDeChamados = _triagemDeChamados;
            this.groupOptions = groupOptions.Value;
        }

        public async Task Executar()
        {
            await _buscarChamados.buscarChamados(groupOptions.WorkGroup);
            await _atribuirChamados.atribuirChamados();
            await _triagemDeChamados.Avaliar();
            await _limparChamados.PreparaChamado();
           
        }
    }
}