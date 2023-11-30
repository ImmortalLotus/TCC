using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.Domain.Models.Enums
{
    public enum SituacaoChamado
    {
        Buscado,
        Atribuido,
        EsperandoValidacao,
        EsperandoLimpeza,
        ErroNaLimpeza,
        ErroNaBuscaDeUsuario,
        UsuarioSemCpf,
        EsperandoExecucao,
        ErroImprevistoNoSei,
        ErroPrevisto,
        ErroNaSolucao,
        EsperandoSolucao,
        EsperandoDesatribuicao,
        ErroTratado,
        Solucionado,
        ErroImprevisto,
        Recusado,
        CategoriaInvalida,
        EsperandoDesatribuicaoCategoria,
        EsperandoDesatribuicaoRecusa,
        Desatribuido,
        NoGrupoDeValidacao,
        Desaprovado,
        Finalizado,
        ErroImprevistoDesatribuir
    }
}
