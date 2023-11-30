using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.Domain.Models.Enums
{
    public enum CodigosDeStatus
    {
        NaoAchou = 404,
        SemPermissao=403,
        Ok=200,
        Waiting=202,
        Recusado= 401,
        ErroImprevisto=500
    }
}
