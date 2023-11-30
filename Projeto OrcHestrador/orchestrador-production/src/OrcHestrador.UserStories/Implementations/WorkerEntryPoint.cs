
using OrcHestrador.Domain.Models;
using OrcHestrador.UserStories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.UserStories.Implementations
{
    public class WorkerEntryPoint : IWorkerEntryPoint
    {
        private readonly IAvaliarChamados _extrairChamados;
        private readonly IExecutarChamado _executarChamado;
        private readonly ISolucionarChamado _solucionarChamados;
        private readonly IValidarChamados _validarChamados;
        private readonly IDesatribuirChamados _desatribuirChamados;

        public WorkerEntryPoint(
            IAvaliarChamados extrairChamados,
            IExecutarChamado executarChamado,
            ISolucionarChamado solucionarChamados,
            IValidarChamados validarChamados,
            IDesatribuirChamados desatribuirChamados)
        {
            this._extrairChamados = extrairChamados;
            this._executarChamado = executarChamado;
            this._solucionarChamados = solucionarChamados;
            this._validarChamados = validarChamados;
            this._desatribuirChamados = desatribuirChamados;
        }
        public async Task exec(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _extrairChamados.Executar();

                await _executarChamado.Executar();

                await _solucionarChamados.Executar();
                await _desatribuirChamados.desatribuirChamados();
                await _validarChamados.ValidaChamados();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
